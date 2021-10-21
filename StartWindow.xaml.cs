using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using DeskCLogic;
using System.Collections.ObjectModel;

namespace DeskC
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        
        public ObservableCollection<TaskModel> ToDo = new ObservableCollection<TaskModel>();
        public ObservableCollection<TaskModel> Doing = new ObservableCollection<TaskModel>();
        public ObservableCollection<TaskModel> Done = new ObservableCollection<TaskModel>();
        public ObservableCollection<TaskModel> Canceled = new ObservableCollection<TaskModel>();

        List<UsersModel> ludzie = new List<UsersModel>();
        List<TaskEnumModel> statusy = new List<TaskEnumModel>();

        public StartWindow()
        {
            InitializeComponent();
            DataContext = new TaskModel();
            userLabel.Content = "Jesteś zalogowany jako " + Session.Login;
            Refresh();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonDodaj_Click(object sender, RoutedEventArgs e)
        {
            var task = new TaskModel();
            task.fullText = fullText.Text;
            task.shortText = shortText.Text;
            task.statusId = MapujStatus(status.Text);
            task.usId = Session.Id;
            DodajZadanie(task);
            //task.usId = Convert.ToInt32(Session.Id);
            
            Refresh();
        }
        private void DodajZadanie(TaskModel task)
        {
            if (fullText.Text.Length != 0 && shortText.Text.Length != 0)
            {
                Zadania.AddTask(task);
                Wyczysc();
            }
            else
                MessageBox.Show("Uzupełnij wszystkie pola!");
        } 
        private void ButtonUsun_Click(object sender, RoutedEventArgs e)
        {

            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += bgWorker_DoWorkDelete;
            bgWorker.RunWorkerCompleted += bgWorker_WorkComplete;

            if (!bgWorker.IsBusy)
            {
                bgWorker.RunWorkerAsync();
            }
            Refresh();
        }
        private void bgWorker_DoWorkDelete(object sender, DoWorkEventArgs e)
        {
            delete();
        }

        private void bgWorker_WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                System.Windows.MessageBox.Show(e.Error.Message);
            }
        }

        private void delete()
        {
            var tasksToDelete = from s in ToDo
                                where s.Selected == true
                                select s.Id;
            foreach (var task in tasksToDelete)
            {
                Zadania.DeleteTask(task);

            }
            MessageBox.Show("Usunięto!");
            //MainWindow.us.ExecuteCommand(206);

            Refresh();
        }

        private int MapujStatus(string status)
        {
            switch (status)
            {
                case "Nowe":
                    return 1;
                case "W trakcie":
                    return 2;
                case "Zakończone":
                    return 3;
                case "Anulowane":
                    return 4;
            }

            return 1;
        }

        private void Refresh()
        {
            ToDo = null;
            statusy = SqliteDataAccess.LoadTaskEnum();
            ludzie = SqliteDataAccess.LoadPeople();
            ToDo = SqliteDataAccess.LoadToDo(Session.Id);
            Doing = SqliteDataAccess.LoadDoing(Session.Id);
            Done = SqliteDataAccess.LoadDone(Session.Id);
            Canceled = SqliteDataAccess.LoadCanceled(Session.Id);

            Dispatcher.Invoke(new Action(() => BindData()));

        }
        private void BindData()
        {
            myTaskTodo.ItemsSource = null;
            myTaskTodo.ItemsSource = ToDo;

            mytaskDoing.ItemsSource = null;
            mytaskDoing.ItemsSource = Doing;

            mytaskDone.ItemsSource = null;
            mytaskDone.ItemsSource = Done;

            mytaskCanceled.ItemsSource = null;
            mytaskCanceled.ItemsSource = Canceled;

            status.ItemsSource = statusy;
            status.DisplayMemberPath = "Status";
            status.SelectedIndex = 0;

        }

        private void Wyczysc()
        {
            fullText.Text = "";
            shortText.Text = "";
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void myTaskTodo_DoubleClick(object sender, EventArgs e)
        {
            if (myTaskTodo.SelectedItem != null)
            {
                MessageBox.Show(myTaskTodo.SelectedItem.ToString());
            }
        }
        private void myTaskDoing_DoubleClick(object sender, EventArgs e)
        {
            if (mytaskDoing.SelectedItem != null)
            {
                MessageBox.Show(mytaskDoing.SelectedItem.ToString());
            }
        }
    }
}

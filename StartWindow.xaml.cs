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
using System.Threading;

namespace DeskC
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public static int trybUsuwania = 1;
        public static int trybEdycji = 1;
        public static int idDoUsuniecia;
        public ObservableCollection<TaskModel> ToDo = new ObservableCollection<TaskModel>();
        public ObservableCollection<TaskModel> Doing = new ObservableCollection<TaskModel>();
        public ObservableCollection<TaskModel> Done = new ObservableCollection<TaskModel>();
        public ObservableCollection<TaskModel> Canceled = new ObservableCollection<TaskModel>();
        public ObservableCollection<TaskModel> All = new ObservableCollection<TaskModel>();

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


        private void zapisz_Click(object sender, RoutedEventArgs e)
        {
            var task = new TaskModel();
            task.fullText = fullText.Text;
            task.shortText = shortText.Text;
            task.statusId = MapujStatus(status.Text);
            task.usId = Session.Id;
            task.Id = idDoUsuniecia;
            AktualizujZadanie(task);

            Wyczysc();
            Refresh();
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
                Logic.DataAccess.AddTask.DodajZadanie(task);
                Wyczysc();
            }
            else
                MessageBox.Show("Uzupełnij wszystkie pola!");
        }

        private void AktualizujZadanie(TaskModel task)
        {
            if (fullText.Text.Length != 0 && shortText.Text.Length != 0)
            {

                Logic.DataAccess.UpdateTask.ZaktualizujZadanie(task);
            }
            else
                MessageBox.Show("Uzupełnij wszystkie pola!");
        }

        private void bgWorker_DoWorkDelete(object sender, DoWorkEventArgs e)
        {
            delete(trybUsuwania);
        }

        private void bgWorker_DoWorkEdit(object sender, DoWorkEventArgs e)
        {
            edit(trybEdycji);
        }

        private void bgWorker_WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                System.Windows.MessageBox.Show(e.Error.Message);
            }
        }

        private void delete(int trybUsuwania)
        {

            switch(trybUsuwania)
            {
                case 1:
                    {
                        var tasksToDeleteToDo = from s in ToDo
                                                where s.Selected == true
                                                select s.Id;
                        foreach (var task in tasksToDeleteToDo)
                        {
                            Logic.DataAccess.DeleteTask.UsunZadanie(task);
                        }
                        break;
                    }
                case 2:
                    {
                        var tasksToDeleteDoing = from s in Doing
                                                 where s.Selected == true
                                                 select s.Id;
                        foreach (var task in tasksToDeleteDoing)
                        {
                            Logic.DataAccess.DeleteTask.UsunZadanie(task);
                        }
                        break;
                    }
                case 3:
                    {
                        var tasksToDeleteDone = from s in Done
                                                where s.Selected == true
                                                select s.Id;
                        foreach (var task in tasksToDeleteDone)
                        {
                            Logic.DataAccess.DeleteTask.UsunZadanie(task);
                        }
                        break;
                    }
                case 4:
                    {
                        var tasksToDeleteCanceled = from s in Canceled
                                                    where s.Selected == true
                                                    select s.Id;
                        foreach (var task in tasksToDeleteCanceled)
                        {
                            Logic.DataAccess.DeleteTask.UsunZadanie(task);
                        }
                        break;
                    }
                    
            }
            MessageBox.Show("Usunięto!");
            //MainWindow.us.ExecuteCommand(206);

            Refresh();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e )
        {

        }

        private void edit(int trybEdycji)
        {
            switch(trybEdycji)
            {
                case 1:
                    {
                        var taskToEdit = from s in ToDo
                                         where s.Selected == true
                                         select s;
                        if (taskToEdit.Count() == 1)
                        {
                            idDoUsuniecia = taskToEdit.First().Id;
                            Dispatcher.Invoke(new Action(() => { shortText.Text = taskToEdit.First().shortText; }));
                            Dispatcher.Invoke(new Action(() => { fullText.Text = taskToEdit.First().fullText; }));
                            Dispatcher.Invoke(new Action(() => { status.SelectedIndex = taskToEdit.First().statusId - 1; }));
                        }
                        break;
                    }
                case 2:
                    {
                        var taskToEdit = from s in Doing
                                         where s.Selected == true
                                         select s;
                        if (taskToEdit.Count() == 1)
                        {
                            idDoUsuniecia = taskToEdit.First().Id;
                            Dispatcher.Invoke(new Action(() => { shortText.Text = taskToEdit.First().shortText; }));
                            Dispatcher.Invoke(new Action(() => { fullText.Text = taskToEdit.First().fullText; }));
                            Dispatcher.Invoke(new Action(() => { status.SelectedIndex = taskToEdit.First().statusId - 1; }));
                        }
                        
                        break;
                    }
                case 3:
                    {
                        var taskToEdit = from s in Done
                                         where s.Selected == true
                                         select s;
                        if (taskToEdit.Count() == 1)
                        {
                            idDoUsuniecia = taskToEdit.First().Id;
                            Dispatcher.Invoke(new Action(() => { shortText.Text = taskToEdit.First().shortText; }));
                            Dispatcher.Invoke(new Action(() => { fullText.Text = taskToEdit.First().fullText; }));
                            Dispatcher.Invoke(new Action(() => { status.SelectedIndex = taskToEdit.First().statusId - 1; }));
                        }
                        break;
                    }
                case 4:
                    {
                        var taskToEdit = from s in Canceled
                                         where s.Selected == true
                                         select s;
                        if (taskToEdit.Count() == 1)
                        {
                            idDoUsuniecia = taskToEdit.First().Id;
                            Dispatcher.Invoke(new Action(() => { shortText.Text = taskToEdit.First().shortText; }));
                            Dispatcher.Invoke(new Action(() => { fullText.Text = taskToEdit.First().fullText; }));
                            Dispatcher.Invoke(new Action(() => { status.SelectedIndex = taskToEdit.First().statusId - 1; }));
                        }    
                        
                        break;
                    }
                default:
                    {
                        var taskToEdit = from s in ToDo
                                         where s.Selected == true
                                         select s;
                        idDoUsuniecia = taskToEdit.First().Id;
                        Dispatcher.Invoke(new Action(() => { shortText.Text = taskToEdit.First().shortText; }));
                        Dispatcher.Invoke(new Action(() => { fullText.Text = taskToEdit.First().fullText; }));
                        Dispatcher.Invoke(new Action(() => { status.SelectedIndex = taskToEdit.First().statusId - 1; }));
                        break;
                    }
            }
            UkryjButton();

        }


        private void UkryjButton()
        {
            Dispatcher.Invoke(new Action(() => { dodaj.Visibility = Visibility.Hidden; }));
            Dispatcher.Invoke(new Action(() => { zapisz.Visibility = Visibility.Visible; }));

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
            statusy = Logic.DataAccess.LoadTasks.LoadTaskEnum();
            ludzie = Logic.DataAccess.LoadPeople.PobierzUzytkownikow();
            ToDo = Logic.DataAccess.LoadTasks.LoadToDo(Session.Id);
            Doing = Logic.DataAccess.LoadTasks.LoadDoing(Session.Id);
            Done = Logic.DataAccess.LoadTasks.LoadDone(Session.Id);
            Canceled = Logic.DataAccess.LoadTasks.LoadCanceled(Session.Id);
            All = Logic.DataAccess.LoadTasks.LoadAll(Session.Id);   
            Dispatcher.Invoke(new Action(() => BindData()));
            Dispatcher.Invoke(new Action(() => { dodaj.Visibility = Visibility.Visible; }));
            Dispatcher.Invoke(new Action(() => { zapisz.Visibility = Visibility.Hidden; }));
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

        private void usun_Todo_Click(object sender, RoutedEventArgs e)
        {
            trybUsuwania = 1;
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += bgWorker_DoWorkDelete;
            bgWorker.RunWorkerCompleted += bgWorker_WorkComplete;

            if (!bgWorker.IsBusy)
            {
                bgWorker.RunWorkerAsync();
            }
            Refresh();
        }

        private void usun_Doing_Click(object sender, RoutedEventArgs e)
        {
            trybUsuwania = 2;
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += bgWorker_DoWorkDelete;
            bgWorker.RunWorkerCompleted += bgWorker_WorkComplete;

            if (!bgWorker.IsBusy)
            {
                bgWorker.RunWorkerAsync();
            }
            Refresh();
        }

        private void usun_Done_Click(object sender, RoutedEventArgs e)
        {
            trybUsuwania = 3;
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += bgWorker_DoWorkDelete;
            bgWorker.RunWorkerCompleted += bgWorker_WorkComplete;

            if (!bgWorker.IsBusy)
            {
                bgWorker.RunWorkerAsync();
            }
            Refresh();
        }

        private void usun_Canceled_Click(object sender, RoutedEventArgs e)
        {
            trybUsuwania = 4;
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += bgWorker_DoWorkDelete;
            bgWorker.RunWorkerCompleted += bgWorker_WorkComplete;

            if (!bgWorker.IsBusy)
            {
                bgWorker.RunWorkerAsync();
            }
            Refresh();
        }

        private void edytuj_todo_Click(object sender, RoutedEventArgs e)
        {
            trybEdycji = 1;
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += bgWorker_DoWorkEdit;
            bgWorker.RunWorkerCompleted += bgWorker_WorkComplete;

            if (!bgWorker.IsBusy)
            {
                bgWorker.RunWorkerAsync();
            }
            Refresh();
        }

        private void edytuj_doing_Click(object sender, RoutedEventArgs e)
        {
            trybEdycji = 2;
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += bgWorker_DoWorkEdit;
            bgWorker.RunWorkerCompleted += bgWorker_WorkComplete;

            if (!bgWorker.IsBusy)
            {
                bgWorker.RunWorkerAsync();
            }
            Refresh();
        }

        private void edytuj_done_Click(object sender, RoutedEventArgs e)
        {
            trybEdycji = 3;
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += bgWorker_DoWorkEdit;
            bgWorker.RunWorkerCompleted += bgWorker_WorkComplete;

            if (!bgWorker.IsBusy)
            {
                bgWorker.RunWorkerAsync();
            }
            Refresh();
        }

        private void edytuj_canceled_Click(object sender, RoutedEventArgs e)
        {
            trybEdycji = 4;
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += bgWorker_DoWorkEdit;
            bgWorker.RunWorkerCompleted += bgWorker_WorkComplete;

            if (!bgWorker.IsBusy)
            {
                bgWorker.RunWorkerAsync();
            }
            Refresh();
        }

        private void wyloguj_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Close();
            main.Show();
        }
    }
}

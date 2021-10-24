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
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        public Details()
        {

            InitializeComponent();
            //Dispatcher.Invoke(new Action(() => InitializeComponent()));
            //BackgroundWorker bgWorker = new BackgroundWorker();
            //bgWorker.DoWork += bgWorker_DoWork;
            //bgWorker.RunWorkerCompleted += bgWorker_WorkComplete;

            //if (!bgWorker.IsBusy)
            //{
            //    bgWorker.RunWorkerAsync();
            //}
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        private void bgWorker_WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void UzupelnijDane(IEnumerable<TaskModel> tasksToDelete)
        {
            
            TaskModel taskDoEdycji = new TaskModel();

        }

        private void save_edit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

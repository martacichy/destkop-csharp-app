﻿using System;
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskC
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        List<TaskModel> ToDo = new List<TaskModel>();
        List<TaskModel> Doing = new List<TaskModel>();
        List<TaskModel> Done = new List<TaskModel>();
        List<TaskModel> Canceled = new List<TaskModel>();

        List<UsersModel> ludzie = new List<UsersModel>();
        List<TaskEnumModel> statusy = new List<TaskEnumModel>();


        public StartWindow()
        {
            InitializeComponent();
            DataContext = new TaskModel();

            Refresh();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var task = new TaskModel();
            task.fullText = fullText.Text;
            task.shortText = shortText.Text;
            task.statusId = MapujStatus(status.Text);
            SqliteDataAccess.AddTask(task);
            Wyczysc();
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
            statusy = SqliteDataAccess.LoadTaskEnum();
            ludzie = SqliteDataAccess.LoadPeople();
            ToDo = SqliteDataAccess.LoadToDo();
            Doing = SqliteDataAccess.LoadDoing();
            Done = SqliteDataAccess.LoadDone();
            Canceled = SqliteDataAccess.LoadDone();

            WypiszZadania();
        }

        private void WypiszZadania()
        {
            taskTodo.ItemsSource = ToDo;
            taskDoing.ItemsSource = Doing;
            taskDone.ItemsSource = Done;
            taskCanceled.ItemsSource = Canceled;
            status.ItemsSource = statusy;

            taskTodo.DisplayMemberPath = "TaskToDisplay";
            taskDoing.DisplayMemberPath = "TaskToDisplay";
            taskDone.DisplayMemberPath = "TaskToDisplay";
            taskCanceled.DisplayMemberPath = "TaskToDisplay";

            status.DisplayMemberPath = "Status";
            status.SelectedIndex = 1;
            //listPeopleListBox.DataSource = null;
            //listPeopleListBox.DataSource = people;
        }

        private void Wyczysc()
        {
            fullText.Text = "";
            shortText.Text = "";
        }
    }
}

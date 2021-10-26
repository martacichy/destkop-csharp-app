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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.ServiceProcess;
using Squirrel;

namespace DeskC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ServiceController us;
        private string[] stringArray;

        public MainWindow()
        {
            us = new ServiceController(Properties.Settings.Default.nameService);
            InitializeComponent();
            stringArray = new string[2];
            stringArray[0] = "login";
            stringArray[1] = "status";
            us.Start(stringArray);
            CheckForUpdates();
        }

        private async Task CheckForUpdates()
        {
            using (var manager = new UpdateManager("C:\\Temp\\Releases"))
            {
                await manager.UpdateApp();
            }
        }

        private void zaloguj_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text.Trim() == "" || password.Password.Trim() == "")
            {
                MessageBox.Show("Uzupełnij login oraz hasło!");
            }
            else
            {
                List<string> Data = Auth(login.Text, password.Password.Trim());
                if (Data.Count > 0)
                {
                    Session.Id = Data[0];
                    Session.Login = Data[1];

                    StartWindow main = new StartWindow();
                    Close();
                    main.Show();

                    stringArray[0] = Session.Login;

                    MainWindow.us.ExecuteCommand(210);

                }
                else
                {
                    MessageBox.Show("Blad logowania.");
                }

            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }


        public static List<string> Auth(string User, string Password)
        {

            SQLiteConnection connection = new SQLiteConnection(LoadConnectionString());
            connection.Open();
            
            List<string> Return = new List<string>();
            string query = "SELECT * FROM Users WHERE Login = @login AND Password = @password LIMIT 1";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            cmd.Parameters.AddWithValue("@login", User);
            cmd.Parameters.AddWithValue("@password", Password);
            SQLiteDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                Return.Add(dataReader["Id"] + "");
                Return.Add(dataReader["Login"] + "");
            }
            dataReader.Close();

            return Return;
        }
    }
}

using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskCLogic;
using System.Collections.ObjectModel;


namespace DeskC
{
    public class SqliteDataAccess
    {
        public static List<UsersModel> LoadPeople()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<UsersModel>("select * from Users", new DynamicParameters());
                return output.ToList();
            }
        }

        public static ObservableCollection<TaskModel> LoadAll(string ID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TaskModel>("select * from Tasks WHERE usId =" + ID + ";").ToList();

                foreach (var d in output)
                {
                    d.TextToDisplay = d.shortText + "\n " + d.fullText;
                }

                ObservableCollection<TaskModel> response = new ObservableCollection<TaskModel>(output);
                return response;
            }
        }

        public static ObservableCollection<TaskModel> LoadToDo(string ID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TaskModel>("select * from Tasks WHERE StatusId = '1' and usId =" + ID + ";").ToList();

                foreach (var d in output)
                {
                    d.TextToDisplay = d.shortText + "\n " + d.fullText;
                }

                ObservableCollection<TaskModel> response = new ObservableCollection<TaskModel>(output);
                return response;
            }
        }

        public static ObservableCollection<TaskModel> LoadDoing(string ID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TaskModel>("select * from Tasks WHERE StatusId = '2' and usId =" + ID + ";").ToList();

                foreach (var d in output)
                {
                    d.TextToDisplay = d.shortText + "\n " + d.fullText;
                }

                ObservableCollection<TaskModel> response = new ObservableCollection<TaskModel>(output);
                return response;
            }
        }
        public static ObservableCollection<TaskModel> LoadDone(string ID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TaskModel>("select * from Tasks WHERE StatusId = '3' and usId =" + ID + ";").ToList();

                foreach (var d in output)
                {
                    d.TextToDisplay = d.shortText + "\n " + d.fullText;
                }

                ObservableCollection<TaskModel> response = new ObservableCollection<TaskModel>(output);
                return response;
            }
        }
        public static ObservableCollection<TaskModel> LoadCanceled(string ID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TaskModel>("select * from Tasks WHERE StatusId = '4' and usId =" + ID + ";").ToList();

                foreach (var d in output)
                {
                    d.TextToDisplay = d.shortText + "\n " + d.fullText;
                }

                ObservableCollection<TaskModel> response = new ObservableCollection<TaskModel>(output);
                return response;
            }
        }
        

        public static List<TaskEnumModel> LoadTaskEnum()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TaskEnumModel>("select * from task_enum", new DynamicParameters());
                return output.ToList();
            }
        }

        
        //public static void SavePerson(PersonModel person)
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //    {
        //        cnn.Execute("insert into Person (FirstName, LastName) values (@FirstName, @LastName)", person);
        //    }
        //}

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}

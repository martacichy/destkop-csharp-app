using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static List<TaskModel> LoadToDo()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TaskModel>("select * from Tasks WHERE StatusId = '1'", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<TaskModel> LoadDoing()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TaskModel>("select * from Tasks WHERE StatusId = 2", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<TaskModel> LoadDone()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TaskModel>("select * from Tasks WHERE StatusId = 3", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<TaskModel> LoadCanceled()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TaskModel>("select * from Tasks WHERE StatusId = 4", new DynamicParameters());
                return output.ToList();
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

        public static void AddTask(TaskModel task)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Tasks (shortText, fullText, statusId, usId) values (@shortText, @fullText, @statusId, 1)", task);
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

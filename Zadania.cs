using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskC;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using DeskCLogic;

namespace DeskC
{
    public class Zadania
    {
        public static void AddTask(TaskModel task)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Tasks (shortText, fullText, statusId, usId) values (@shortText, @fullText, @statusId, @usId)", task);
            }
        }

        public static void DeleteTask(int taskId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = $"DELETE FROM Tasks WHERE Id = {taskId}";
                cnn.Execute(sql);
            }
        }

        public static void UpdateTask(TaskModel task)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = $"UPDATE Tasks SET shortText = @shortText, fullText = @fullText, statusId = @statusId WHERE id = @Id";
                cnn.Execute(sql, task);
            }
        }


         private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}

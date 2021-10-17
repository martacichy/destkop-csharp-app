using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskC;
using System;
using System.Collections.Generic;
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
                cnn.Execute("delete from Tasks WHERE Id = '{0}'", taskId);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}

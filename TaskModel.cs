using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskC
{
    public class TaskModel
    {
        public int Id { get; set; }
        public int usId { get; set; }
        public string shortText { get; set; }
        public string fullText { get; set; }
        public int statusId { get; set; }


        public string TaskToDisplay
        {
            get
            {
                return $"{ shortText } \n { fullText }";
            }
        }
    }
}
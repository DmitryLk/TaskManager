using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerLib.Models
{
    public class TaskEventArgs
    {
        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; }

        public TaskEventArgs(string mes)
        {
            Message = mes;
        }
    }
}
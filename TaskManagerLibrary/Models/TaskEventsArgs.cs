using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Models
{
    internal class TaskEventArgs
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
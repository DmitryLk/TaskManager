using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Models
{
    class TaskEventArgs
    {
        // Сообщение
        public string Message { get; }

        public TaskEventArgs(string mes)
        {
            Message = mes;
        }
    }
}

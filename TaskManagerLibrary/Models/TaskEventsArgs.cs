using System;

namespace TaskManagerLib.Models
{
    public class TaskEventArgs : EventArgs
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
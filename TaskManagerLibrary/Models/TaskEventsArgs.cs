using System;
using JetBrains.Annotations;

namespace TaskManagerLib.Models
{
    /// <summary>
    /// Аргументы события
    /// </summary>
    public class TaskEventArgs : EventArgs
    {
        /// <summary>
        /// Текстовое сообщение
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mes"></param>
        public TaskEventArgs([NotNull]string mes)
        {
            Message = mes;
        }
    }
}
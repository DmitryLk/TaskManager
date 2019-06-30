using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerLib.Models
{
    /// <summary>
    /// Событие задачи
    /// </summary>
    public class TaskEvent
    {
        private readonly List<Action<object, TaskEventArgs>> _handlerList = new List<Action<object, TaskEventArgs>>();
        private static readonly object locker = new object();

        /// <summary>
        /// Сработка события (FIFO)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Invoke(object sender, TaskEventArgs e)
        {
            if (_handlerList != null)
            {
                foreach (var handler in _handlerList)
                {
                    handler.Invoke(sender, e);
                }
            }
        }

        /// <summary>
        /// Перегрузка оператора +
        /// </summary>
        /// <param name="taskEvent"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TaskEvent operator +(TaskEvent taskEvent, Action<object, TaskEventArgs> action)
        {
            taskEvent.AddHandler(action);
            return taskEvent;
        }

        /// <summary>
        /// Перегрузка оператора -
        /// </summary>
        /// <param name="taskEvent"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TaskEvent operator -(TaskEvent taskEvent, Action<object, TaskEventArgs> action)
        {
            taskEvent.RemoveHandler(action);
            return taskEvent;
        }

        /// <summary>
        /// Добавление обработчика
        /// </summary>
        /// <param name="action"></param>
        public void AddHandler(Action<object, TaskEventArgs> action)
        {
            lock (locker)
            {
                _handlerList.Add(action);
            }
        }

        /// <summary>
        /// Удаление обработчика
        /// </summary>
        /// <param name="action"></param>
        public void RemoveHandler(Action<object, TaskEventArgs> action)
        {
            lock (locker)
            {
                _handlerList.Remove(action);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerLib.Models
{
    public class TaskEvent
    {
        private readonly List<Action<object, TaskEventArgs>> _handlerList = new List<Action<object, TaskEventArgs>>();
        private static readonly object locker = new object();

        //проверка на null
        internal void Invoke(object sender, TaskEventArgs e)
        {
            foreach (var handler in _handlerList)
            {
                handler.Invoke(sender, e);
            }
        }

        public static TaskEvent operator +(TaskEvent taskEvent, Action<object, TaskEventArgs> action)
        {
            taskEvent.AddHandler(action);
            return taskEvent;
        }

        public static TaskEvent operator -(TaskEvent taskEvent, Action<object, TaskEventArgs> action)
        {
            taskEvent.AddHandler(action);
            return taskEvent;
        }

        public void AddHandler(Action<object, TaskEventArgs> action)
        {
            lock (locker)
            {
                _handlerList.Add(action);
            }
        }

        public void RemoveHandler(Action<object, TaskEventArgs> action)
        {
            lock (locker)
            {
                _handlerList.Remove(action);
            }
        }
    }
}

/*
//private Action<string> TaskEvent(string message);
//public delegate void TaskStateHandler(object sender, TaskEventArgs e);
//public delegate void TaskEvent(string message);
//public event TaskStateHandler Creating;
//public event Action<object, TaskEventArgs> Creating2;

public delegate void TaskStateHandler(object sender, TaskEventArgs e);

private EventHandler _MyEvent; // закрытое поле обработчика

public event EventHandler MyEvent
{
    add { lock (this) { _MyEvent += value; } }
    remove { lock (this) { _MyEvent -= value; } }
}

for (int ctr = outputMessage.GetInvocationList().Length - 1; ctr >= 0; ctr--)
{
    var outputMsg = outputMessage.GetInvocationList()[ctr];
    outputMsg.DynamicInvoke("Greetings and salutations!");
}

 */
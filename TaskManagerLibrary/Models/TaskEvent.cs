using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Models
{
    internal class TaskEvent
    {
        private List<Action<object, TaskEventArgs>> _handlerList;

        //проверка на null
        public void DispatchEvent(object sender, TaskEventArgs e)
        {
            foreach (var handler in _handlerList)
            {
                handler.Invoke(sender, e);
            }
        }

        public void AddHandler(Action<object, TaskEventArgs> action)
        {
            _handlerList.Add(action);
        }

        public void RemoveHandler(Action<object, TaskEventArgs> action)
        {
            _handlerList.Remove(action);
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
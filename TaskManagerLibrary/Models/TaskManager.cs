using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TaskManager.Enums;

namespace TaskManager.Models
{
    public class TaskManager : ITaskManager
    {
        private readonly Dictionary<TaskPriority, ConcurrentQueue<Task>> _taskQueueDictionary;
        private readonly Array _priorities;
        private bool IsStarted = false;
        private bool IsBusy = false;
        private static object locker = new object();

        public TaskManager()
        {
            var _taskQueueDictionary = new Dictionary<TaskPriority, ConcurrentQueue<Task>>();
            _priorities = Enum.GetValues(typeof(TaskPriority));
            Array.Reverse(_priorities);
        }

        public void Enqueue(Task task)
        {
            task.EndExecution.AddHandler(EndNextTaskExecution);
            task.Error.AddHandler(EndNextTaskExecution);
            _taskQueueDictionary[task.Priority].Enqueue(task);
            if (!IsBusy && IsStarted)
            {
                lock (locker)
                {
                    if (!IsBusy && IsStarted)
                    {
                        RunNextTask();
                    }
                }
            }
        }

        public void StartQueue()
        {
            if (!IsStarted)
            {
                lock (locker)
                {
                    if (!IsStarted)
                    {
                        IsStarted = true;
                        RunNextTask();
                    }
                }
            }
        }

        private void EndNextTaskExecution(object sender, TaskEventArgs e)
        {
            if (IsStarted) RunNextTask();
        }

        private void RunNextTask()
        {
            if (IsBusy = TryGetNextTask(out Task task)) task.RunAsync();
        }

        public void StopQueue()
        {
            IsStarted = false;
        }

        private bool TryGetNextTask(out Task task)
        {
            task = null;
            foreach (var priority in _priorities)
            {
                var queue = _taskQueueDictionary[(TaskPriority)priority];
                if (queue.TryDequeue(out Task taskQ))
                {
                    task = taskQ;
                    return true;
                }
            }
            return false;
        }
    }
}
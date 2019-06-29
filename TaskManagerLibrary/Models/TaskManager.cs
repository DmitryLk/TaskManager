using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using JetBrains.Annotations;
using TaskManagerLib.Enums;

namespace TaskManagerLib.Models
{
    public class TaskManager : ITaskManager
    {
        private readonly Dictionary<TaskPriority, ConcurrentQueue<Task>> _taskQueueDictionary;
        private readonly Array _priorities;

        public bool IsStarted { get; private set; }
        public bool IsBusy { get; private set; }

        private static readonly object locker = new object();

        public TaskManager()
        {
            _priorities = Enum.GetValues(typeof(TaskPriority));
            Array.Reverse(_priorities);

            _taskQueueDictionary = new Dictionary<TaskPriority, ConcurrentQueue<Task>>();
            foreach (var priority in _priorities)
            {
                _taskQueueDictionary.Add((TaskPriority)priority, new ConcurrentQueue<Task>());
            }
        }

        public bool TaskEnqueue([NotNull]Task task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            task.EndExecution += EndNextTaskExecution;
            task.Error += EndNextTaskExecution;

            _taskQueueDictionary[task.Priority].Enqueue(task);

            if (!IsBusy && IsStarted)
            {
                lock (locker)
                {
                    if (!IsBusy && IsStarted)
                    {
                        RunNextTask();
                        return true;
                    }
                }
            }
            return false;
        }

        public bool StartQueue()
        {
            if (!IsStarted)
            {
                lock (locker)
                {
                    if (!IsStarted)
                    {
                        IsStarted = true;
                        if (!IsBusy) RunNextTask();
                        return true;
                    }
                }
            }

            return false;
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
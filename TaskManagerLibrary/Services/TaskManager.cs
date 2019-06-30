using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using JetBrains.Annotations;
using TaskManagerLib.Enums;
using TaskManagerLib.Models;
using TaskManagerLib.Ports;

namespace TaskManagerLib.Services
{
    /// <summary>
    /// Сервис управления задачами
    /// </summary>
    public class TaskManager : ITaskManager
    {
        private readonly Dictionary<TaskPriority, ConcurrentQueue<ITask>> _taskQueueDictionary;
        private readonly Array _priorities;
        private static readonly object locker = new object();

        public bool IsStarted { get; private set; }
        public bool IsBusy { get; private set; }

        /// <summary>
        /// ctor
        /// </summary>
        public TaskManager()
        {
            _priorities = Enum.GetValues(typeof(TaskPriority));
            Array.Reverse(_priorities);

            _taskQueueDictionary = new Dictionary<TaskPriority, ConcurrentQueue<ITask>>();
            foreach (var priority in _priorities)
            {
                _taskQueueDictionary.Add((TaskPriority)priority, new ConcurrentQueue<ITask>());
            }
        }

        public bool TaskEnqueue(ITask task)
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

        public void StopQueue()
        {
            IsStarted = false;
        }

        private void EndNextTaskExecution(object sender, TaskEventArgs e)
        {
            if (IsStarted) RunNextTask();
        }

        private void RunNextTask()
        {
            if (IsBusy = TryGetNextTask(out ITask task)) task.RunAsync();
        }

        private bool TryGetNextTask(out ITask task)
        {
            task = null;
            foreach (var priority in _priorities)
            {
                var queue = _taskQueueDictionary[(TaskPriority)priority];
                if (queue.TryDequeue(out ITask taskQ))
                {
                    task = taskQ;
                    return true;
                }
            }
            return false;
        }
    }
}
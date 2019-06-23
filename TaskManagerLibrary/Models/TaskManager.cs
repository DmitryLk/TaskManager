using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TaskManager.Enums;

namespace TaskManager.Models
{
    public class TaskManager
    {
        private readonly Dictionary<TaskPriority, ConcurrentQueue<Task>> _taskQueueDictionary;

        public TaskManager()
        {
            var _taskQueueDictionary = new Dictionary<TaskPriority, ConcurrentQueue<Task>>();
        }

        public void Enqueue(Task task)
        {
            _taskQueueDictionary[task.Priority].Enqueue(task);
        }

        public void StartQueue()
        {
            ConcurrentQueue<Task> queue;
            var priorities = Enum.GetValues(typeof(TaskPriority));
            Array.Reverse(priorities);

            foreach (var priority in priorities)
            {
                queue = _taskQueueDictionary[(TaskPriority)priority];
                while (queue.TryDequeue(out Task task))
                {
                    task.StartTask();
                }
            }
        }
    }
}
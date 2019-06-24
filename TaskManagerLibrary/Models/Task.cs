using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManagerLib.Enums;

using System;

using System.Threading;

using System.Threading.Tasks;

namespace TaskManagerLib.Models
{
    public class Task
    {
        /// <summary>
        /// Название задачи
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Содержимое задачи
        /// </summary>
        public string Content { get; private set; }

        private static Random rand = new Random();

        public TaskType Type { get; private set; }
        public TaskPriority Priority { get; private set; }
        public TaskEvent Creating { get; set; } = new TaskEvent();
        public TaskEvent BeginExecution { get; set; } = new TaskEvent();
        public TaskEvent EndExecution { get; set; } = new TaskEvent();
        public TaskEvent Error { get; set; } = new TaskEvent();

        public Task(string name, string content, TaskPriority priority, TaskType type)
        {
            Name = name;
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Priority = priority;
            Type = type;
            Creating?.Invoke(this, new TaskEventArgs($"Создание задачи {Name} Priority:{Priority} Type:{Type}"));
        }

        /// <summary>
        /// Выполнение задачи
        /// </summary>
        public async void RunAsync()
        {
            BeginExecution?.Invoke(this, new TaskEventArgs($"Начало выполнения задачи {Name} Priority:{Priority} Type:{Type}"));

            //todo: выполнение задачи
            //Console.WriteLine(Content);
            await System.Threading.Tasks.Task.Delay(500);
            if (rand.Next(1, 10) > 1)
            {
                EndExecution?.Invoke(this, new TaskEventArgs($"Окончание выполнения задачи {Name} Priority:{Priority} Type:{Type}"));
            }
            else
            {
                Error?.Invoke(this, new TaskEventArgs($"Ошибка выполнения задачи {Name} Priority:{Priority} Type:{Type}"));
            }
        }

        //private static System.Threading.Tasks.Task Delay(int v)
        //{
        //    throw new NotImplementedException();
        //}

        public void UpdatePriority(TaskPriority priority)
        {
            this.Priority = priority;
        }
    }
}

/*
//public string Content { get; private set; }
//public TaskPriority Priority { get; private set; }
//public TaskType Type { get; private set; }
 */
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Enums;

namespace TaskManager.Models
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
        internal TaskEvent Creating { get; set; }
        internal TaskEvent BeginExecution { get; set; }
        internal TaskEvent EndExecution { get; set; }
        internal TaskEvent Error { get; set; }

        public Task(string name, string content, TaskPriority priority, TaskType type)
        {
            Name = name;
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Priority = priority;
            Type = type;
            Creating.Invoke(this, new TaskEventArgs($"Создание задачи {Priority} {Name} {Type}"));
        }

        /// <summary>
        /// Выполнение задачи
        /// </summary>
        public void Run()
        {
            BeginExecution.Invoke(this, new TaskEventArgs($"Начало выполнения задачи {Priority} {Name} {Type}"));

            //todo: выполнение задачи
            //Console.WriteLine(Content);

            if (rand.Next(1, 10) > 1)
            {
                EndExecution.Invoke(this, new TaskEventArgs($"Окончание выполнения задачи {Priority} {Name} {Type}"));
            }
            else
            {
                Error.Invoke(this, new TaskEventArgs($"Ошибка выполнения задачи {Priority} {Name} {Type}"));
            }
        }

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
using System;
using TaskManagerLib.Enums;
using JetBrains.Annotations;

namespace TaskManagerLib.Models
{
    public class Task : ITask
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

        public Task([NotNull]string name, [NotNull]string content, [NotNull]TaskPriority priority, [NotNull]TaskType type)
        {
            Name = name;
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Priority = priority;
            Type = type;
            Creating?.Invoke(this, new TaskEventArgs($"Задача {Name} Priority:{Priority} Type:{Type} Создание"));
        }

        /// <summary>
        /// Выполнение задачи
        /// </summary>
        public async System.Threading.Tasks.Task<bool> RunAsync()
        {
            bool result;
            BeginExecution?.Invoke(this, new TaskEventArgs($"Задача {Name} Priority:{Priority} Type:{Type} Начало выполнения"));

            //todo: выполнение задачи
            await System.Threading.Tasks.Task.Delay(1000);
            result = rand.Next(1, 10) > 1;

            if (!result)
            {
                Error?.Invoke(this, new TaskEventArgs($"Задача {Name} Priority:{Priority} Type:{Type} Ошибка выполнения\r\n"));
                return result;
            }

            EndExecution?.Invoke(this, new TaskEventArgs($"Задача {Name} Priority:{Priority} Type:{Type} Окончание выполнения\r\n"));
            return result;
        }

        public void UpdatePriority(TaskPriority priority)
        {
            this.Priority = priority;
        }
    }
}
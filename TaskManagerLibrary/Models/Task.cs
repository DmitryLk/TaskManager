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
        private readonly Func<int, int, bool> _contentFunc;

        private readonly int _x;
        private readonly int _y;

        private TaskResult _result;

        public TaskType Type { get; private set; }
        public TaskPriority Priority { get; private set; }

        public TaskEvent Creating { get; set; } = new TaskEvent();
        public TaskEvent BeginExecution { get; set; } = new TaskEvent();
        public TaskEvent EndExecution { get; set; } = new TaskEvent();
        public TaskEvent Error { get; set; } = new TaskEvent();

        public Task([NotNull]string name, int x, int y, [NotNull]Func<int, int, bool> content, TaskPriority priority, TaskType type)
        {
            Name = name;
            _contentFunc = content ?? throw new ArgumentNullException(nameof(content));
            _x = x;
            _y = y;

            Priority = priority;
            Type = type;
            Creating?.Invoke(this, new TaskEventArgs($"Задача {Name} Priority:{Priority} Type:{Type} Создание"));
        }

        /// <summary>
        /// Выполнение задачи
        /// </summary>
        public async System.Threading.Tasks.Task<TaskResult> RunAsync()
        {
            TaskResult result;
            BeginExecution?.Invoke(this, new TaskEventArgs($"Задача {Name} Priority:{Priority} Type:{Type} Начало выполнения"));

            //todo: выполнение задачи
            try
            {
                result = await System.Threading.Tasks.Task.Run(() =>
                {
                    System.Threading.Tasks.Task.Delay(1000);
                    if (_contentFunc(_x, _y))
                    {
                        return TaskResult.CreateSuccess("Успешно");
                    }
                    else
                    {
                        return TaskResult.CreateError(TaskError.False, "Ошибка");
                    }
                });
            }
            catch (Exception ex)
            {
                result = TaskResult.CreateError(TaskError.Exception, "Ошибка"); ;
            }

            if (result.HasError())
            {
                EndExecution?.Invoke(this, new TaskEventArgs($"Задача {Name} Priority:{Priority} Type:{Type} Задача выполнена Result:{result.Message} {result.Error}\r\n"));
            }
            else
            {
                Error?.Invoke(this, new TaskEventArgs($"Задача {Name} Priority:{Priority} Type:{Type} Задача выполнена Result:{result.Message} {result.Error}\r\n"));
            }

            _result = result;
            return result;
        }

        public void UpdatePriority(TaskPriority priority)
        {
            this.Priority = priority;
        }
    }
}
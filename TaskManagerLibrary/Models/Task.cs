using System;
using TaskManagerLib.Enums;
using JetBrains.Annotations;
using System.Linq.Expressions;
using TaskManagerLib.Ports;

namespace TaskManagerLib.Models
{
    /// <summary>
    /// Задача
    /// </summary>
    public class Task : ITask
    {
        public string Name { get; private set; }

        private readonly Expression<Func<int, int, bool>> _contentFunc;
        public TaskType Type { get; private set; }
        public TaskPriority Priority { get; private set; }

        private readonly int _argument1;
        private readonly int _argument2;
        private ITaskResult _result;

        public TaskEvent Creating { get; set; } = new TaskEvent();
        public TaskEvent BeginExecution { get; set; } = new TaskEvent();
        public TaskEvent EndExecution { get; set; } = new TaskEvent();
        public TaskEvent Error { get; set; } = new TaskEvent();

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="названиеи задачи"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="content"></param>
        /// <param name="priority"></param>
        /// <param name="type"></param>
        public Task([NotNull]string name, int argument1, int argument2, [NotNull]Expression<Func<int, int, bool>> content, TaskType type = TaskType.Type1, TaskPriority priority = TaskPriority.VeryLow)
        {
            Name = name;
            _contentFunc = content ?? throw new ArgumentNullException(nameof(content));
            _argument1 = argument1;
            _argument2 = argument2;

            Priority = priority;
            Type = type;
            Creating?.Invoke(this, new TaskEventArgs($"Задача {Name} Priority:{Priority} Type:{Type} Создание"));
        }

        #region Методы

        public async System.Threading.Tasks.Task<ITaskResult> RunAsync()
        {
            ITaskResult result;
            BeginExecution?.Invoke(this, new TaskEventArgs($"Задача {Name} Priority:{Priority} Type:{Type} Начало выполнения"));

            //todo: выполнение задачи
            result = await System.Threading.Tasks.Task.Run(async () =>
            {
                bool lambdaResult;
                await System.Threading.Tasks.Task.Delay(1000);
                try
                {
                    lambdaResult = _contentFunc.Compile().Invoke(_argument1, _argument2);
                }
                catch
                {
                    return TaskResult.CreateError(TaskError.Exception, "Ошибка"); ;
                }

                if (lambdaResult)
                {
                    return TaskResult.CreateSuccess("Успешно");
                }
                else
                {
                    return TaskResult.CreateError(TaskError.False, "Ошибка");
                }
            });

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

        #endregion Методы
    }
}
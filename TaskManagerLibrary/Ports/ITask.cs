using System.Threading.Tasks;
using TaskManagerLib.Models;
using TaskManagerLib.Enums;
using JetBrains.Annotations;

namespace TaskManagerLib.Ports
{
    /// <summary>
    /// Интерфейс задачи
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Название задачи
        /// </summary>
        [NotNull]
        string Name { get; }

        /// <summary>
        /// Выполнение задачи
        /// </summary>
        Task<ITaskResult> RunAsync();

        /// <summary>
        /// Приоритет задачи
        /// </summary>
        TaskPriority Priority { get; }

        /// <summary>
        /// Тип задачи
        /// </summary>
        TaskType Type { get; }

        /// <summary>
        /// Изменить приоритет задачи
        /// </summary>
        /// <param name="priority"></param>
        void UpdatePriority(TaskPriority priority);

        /// <summary>
        /// Событие начала выполнения
        /// </summary>
        TaskEvent BeginExecution { get; set; }

        /// <summary>
        /// Событие создания
        /// </summary>
        TaskEvent Creating { get; set; }

        /// <summary>
        /// Событие окончания выполнения
        /// </summary>
        TaskEvent EndExecution { get; set; }

        /// <summary>
        /// Событие ошибки
        /// </summary>
        TaskEvent Error { get; set; }
    }
}
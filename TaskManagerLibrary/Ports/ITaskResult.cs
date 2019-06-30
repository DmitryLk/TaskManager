using JetBrains.Annotations;
using TaskManagerLib.Enums;

namespace TaskManagerLib.Ports
{
    /// <summary>
    /// Интерфейс результата задачи
    /// </summary>
    public interface ITaskResult
    {
        /// <summary>
        /// Тип ошибки
        /// </summary>
        [CanBeNull]
        TaskError? Error { get; set; }

        /// <summary>
        /// Сообщение ошибки
        /// </summary>
        [NotNull]
        string Message { get; set; }

        /// <summary>
        /// Проверка наличия ошибки
        /// </summary>
        /// <returns></returns>
        bool HasError();
    }
}
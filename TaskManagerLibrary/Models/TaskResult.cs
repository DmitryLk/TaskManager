using JetBrains.Annotations;
using TaskManagerLib.Enums;
using TaskManagerLib.Ports;

namespace TaskManagerLib.Models
{
    /// <summary>
    /// Результат выполнения задачи
    /// </summary>
    public class TaskResult : ITaskResult
    {
        public TaskError? Error { get; set; }

        public string Message { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="error"></param>
        private TaskResult(string message, TaskError error)
        {
            Error = error;
            Message = message;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="message"></param>
        private TaskResult(string message)
        {
            Message = message;
        }

        public bool HasError()
        {
            return Error != null;
        }

        /// <summary>
        /// Фабрика генерации успешного результата
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static TaskResult CreateSuccess(string message)
        {
            return new TaskResult(message);
        }

        /// <summary>
        /// Фабрика генерации неуспешного результата
        /// </summary>
        /// <param name="error"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static TaskResult CreateError(TaskError error, string message)
        {
            return new TaskResult(message, error);
        }
    }
}
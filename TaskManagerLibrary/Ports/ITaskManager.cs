using JetBrains.Annotations;
using TaskManagerLib.Models;

namespace TaskManagerLib.Ports
{
    /// <summary>
    /// Интерфейс сервиса управления задачами
    /// </summary>
    public interface ITaskManager
    {
        /// <summary>
        /// Проверка текущего выполнения очереди задач
        /// </summary>
        bool IsBusy { get; }

        /// <summary>
        /// Проверка очередь задач запущена
        /// </summary>
        bool IsStarted { get; }

        /// <summary>
        /// Запуск вполнения очереди задач
        /// </summary>
        /// <returns></returns>
        bool StartQueue();

        /// <summary>
        /// Остановить выполнение очереди задач
        /// </summary>
        void StopQueue();

        /// <summary>
        /// Добавить задачу в очередь задач
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        bool TaskEnqueue([NotNull] ITask task);
    }
}
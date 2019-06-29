using JetBrains.Annotations;

namespace TaskManagerLib.Models
{
    public interface ITaskManager
    {
        bool IsBusy { get; }
        bool IsStarted { get; }

        bool StartQueue();
        void StopQueue();
        bool TaskEnqueue([NotNull] Task task);
    }
}
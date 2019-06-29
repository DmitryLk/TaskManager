using JetBrains.Annotations;

namespace TaskManagerLib.Models
{
    public interface ITaskManager
    {
        bool StartQueue();
        void StopQueue();
        bool TaskEnqueue([NotNull] Task task);
        bool TaskManagerIsBusy();
        bool TaskManagerIsStarted();
    }
}
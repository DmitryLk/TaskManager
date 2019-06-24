namespace TaskManagerLib.Models
{
    public interface ITaskManager
    {
        void TaskEnqueue(Task task);

        void StartQueue();
    }
}
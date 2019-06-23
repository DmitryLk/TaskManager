namespace TaskManager.Models
{
    public interface ITaskManager
    {
        void Enqueue(Task task);
        void StartQueue();
    }
}
using System.Threading.Tasks;
using TaskManagerLib.Enums;

namespace TaskManagerLib.Models
{
    public interface ITask
    {
        string Name { get; }
        string Content { get; }
        TaskPriority Priority { get; }
        TaskType Type { get; }

        TaskEvent BeginExecution { get; set; }
        TaskEvent Creating { get; set; }
        TaskEvent EndExecution { get; set; }
        TaskEvent Error { get; set; }

        Task<bool> RunAsync();

        void UpdatePriority(TaskPriority priority);
    }
}
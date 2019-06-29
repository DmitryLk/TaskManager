using System.Threading.Tasks;
using TaskManagerLib.Models;
using TaskManagerLib.Enums;

namespace TaskManagerLib.Models
{
    public interface ITask
    {
        string Name { get; }
        TaskPriority Priority { get; }
        TaskType Type { get; }

        Task<TaskResult> RunAsync();

        void UpdatePriority(TaskPriority priority);

        TaskEvent BeginExecution { get; set; }
        TaskEvent Creating { get; set; }
        TaskEvent EndExecution { get; set; }
        TaskEvent Error { get; set; }
    }
}
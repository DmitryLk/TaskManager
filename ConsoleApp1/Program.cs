using System;
using TaskManagerLib.Enums;
using TaskManagerLib.Models;
using TaskManagerLib.Ports;
using TaskManagerLib.Services;

namespace ConsoleApp1
{
    internal class Program
    {
        private static ITaskManager taskManager = new TaskManager();

        private static void Main(string[] args)
        {
            TaskAttach(new Task("task1", 2, 2, (x, y) => x + y == 4, TaskType.Type1, TaskPriority.Middle));
            TaskAttach(new Task("task2", 2, 2, (x, y) => x + y == 4, TaskType.Type1, TaskPriority.High));
            TaskAttach(new Task("task3", 2, 0, (x, y) => x * y == 5, TaskType.Type1, TaskPriority.VeryLow));
            taskManager.StartQueue();
            TaskAttach(new Task("task4", 2, 2, (x, y) => x + y == 4, TaskType.Type1, TaskPriority.VeryHigh));
            TaskAttach(new Task("task5", 2, 2, (x, y) => x + y == 5, TaskType.Type1, TaskPriority.Low));
            taskManager.StopQueue();
            TaskAttach(new Task("task6", 2, 2, (x, y) => x * y == 4, TaskType.Type1, TaskPriority.VeryLow));
            TaskAttach(new Task("task7", 2, 2, (x, y) => x + y == 4, TaskType.Type1, TaskPriority.Middle));
            TaskAttach(new Task("task8", 2, 2, (x, y) => x + y == 5, TaskType.Type1, TaskPriority.Middle));
            TaskAttach(new Task("task9", 2, 0, (x, y) => x / y == 4, TaskType.Type1, TaskPriority.High));
            taskManager.StartQueue();
            taskManager.StopQueue();
            taskManager.StartQueue();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        private static void Print(object sender, TaskEventArgs args)
        {
            Console.WriteLine($"{args.Message}");
        }

        private static void TaskAttach(ITask task)
        {
            task.BeginExecution += Print;
            task.EndExecution += Print;
            task.Error += Print;
            taskManager.TaskEnqueue(task);
        }
    }
}
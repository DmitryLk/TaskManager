using System;
using TaskManagerLib.Enums;
using TaskManagerLib.Models;

namespace ConsoleApp1
{
    internal class Program
    {
        private static ITaskManager taskManager = new TaskManager();

        private static void Main(string[] args)
        {
            TaskAttach(new Task("task1", 2, 2, (x, y) => x + y == 4, TaskPriority.Middle, TaskType.Type1));
            TaskAttach(new Task("task2", 2, 2, (x, y) => x + y == 4, TaskPriority.High, TaskType.Type1));
            TaskAttach(new Task("task3", 2, 0, (x, y) => x * y == 5, TaskPriority.VeryLow, TaskType.Type1));
            taskManager.StartQueue();
            TaskAttach(new Task("task4", 2, 2, (x, y) => x + y == 4, TaskPriority.VeryHigh, TaskType.Type1));
            TaskAttach(new Task("task5", 2, 2, (x, y) => x + y == 5, TaskPriority.Low, TaskType.Type1));
            taskManager.StopQueue();
            TaskAttach(new Task("task6", 2, 2, (x, y) => x * y == 4, TaskPriority.VeryLow, TaskType.Type1));
            TaskAttach(new Task("task7", 2, 2, (x, y) => x + y == 4, TaskPriority.Middle, TaskType.Type1));
            TaskAttach(new Task("task8", 2, 2, (x, y) => x + y == 5, TaskPriority.Middle, TaskType.Type1));
            TaskAttach(new Task("task9", 2, 0, (x, y) => x / y == 4, TaskPriority.High, TaskType.Type1));
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

        private static void TaskAttach(Task task)
        {
            task.BeginExecution += Print;
            task.EndExecution += Print;
            task.Error += Print;
            taskManager.TaskEnqueue(task);
        }
    }
}
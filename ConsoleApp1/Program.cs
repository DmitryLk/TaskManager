using System;
using System.Security.Cryptography;
using TaskManagerLib.Enums;
using TaskManagerLib.Models;

namespace ConsoleApp1
{
    internal class Program
    {
        private static TaskManager taskManager = new TaskManager();

        private static void Main(string[] args)
        {
            TaskAttach(new Task("task1", "task1_content", TaskPriority.Middle, TaskType.Type1));
            TaskAttach(new Task("task2", "task2_content", TaskPriority.High, TaskType.Type1));
            TaskAttach(new Task("task3", "task3_content", TaskPriority.VeryLow, TaskType.Type1));
            taskManager.StartQueue();
            TaskAttach(new Task("task4", "task4_content", TaskPriority.VeryHigh, TaskType.Type1));
            TaskAttach(new Task("task5", "task5_content", TaskPriority.Low, TaskType.Type1));
            TaskAttach(new Task("task6", "task6_content", TaskPriority.VeryLow, TaskType.Type1));
            TaskAttach(new Task("task7", "task7_content", TaskPriority.Middle, TaskType.Type1));
            TaskAttach(new Task("task8", "task8_content", TaskPriority.Middle, TaskType.Type1));
            TaskAttach(new Task("task9", "task9_content", TaskPriority.High, TaskType.Type1));
            //taskManager.StopQueue();
            TaskAttach(new Task("task10", "task10_content", TaskPriority.VeryHigh, TaskType.Type1));

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
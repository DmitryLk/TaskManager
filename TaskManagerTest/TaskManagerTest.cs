using NUnit.Framework;
using System;
using TaskManagerLib.Enums;
using TaskManagerLib.Models;
using TaskManagerLib.Services;

namespace Tests
{
    /// <summary>
    /// Тесты для класса TaskManagerTests
    /// </summary>
    public class TaskManagerTests
    {
        [Test]
        public void StartQueue_QueueNotStarted_ReturnSuccess()
        {
            //Arrange
            var taskManager = new TaskManager();

            //Act
            var result = taskManager.StartQueue();

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void StartQueue_QueueStarted_ReturnFalse()
        {
            //Arrange
            var taskManager = new TaskManager();
            taskManager.StartQueue();

            //Act
            var result = taskManager.StartQueue();

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void TaskEnqueue_QueueNotStarted_ReturnFalse()
        {
            //Arrange
            var taskManager = new TaskManager();
            var task = new Task("test", 2, 2, (x, y) => false, TaskType.Type1, TaskPriority.High);

            //Act
            var result = taskManager.TaskEnqueue(task);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void TaskEnqueue_QueueStarted_ReturnSuccess()
        {
            //Arrange
            var taskManager = new TaskManager();
            taskManager.StartQueue();
            var task = new Task("test", 2, 2, (x, y) => false, TaskType.Type1, TaskPriority.High);

            //Act
            var result = taskManager.TaskEnqueue(task);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void TaskEnqueue_TaskNull_ThrowException()
        {
            //Arrange
            var taskManager = new TaskManager();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => taskManager.TaskEnqueue(null));
        }
    }
}
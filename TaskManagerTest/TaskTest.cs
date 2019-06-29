using NUnit.Framework;
using TaskManagerLib.Enums;
using TaskManagerLib.Models;

namespace Tests
{
    public class TaskTests
    {
        [Test]
        public async System.Threading.Tasks.Task RunAsync_TrueDelegate_ReturnSuccess()
        {
            //Arrange
            var task = new Task("task1", 2, 2, (x, y) => x + y == 4, TaskPriority.Middle, TaskType.Type1);

            //Act
            var result = await task.RunAsync();

            //Assert
            Assert.IsFalse(result.HasError());
        }

        [Test]
        public async System.Threading.Tasks.Task RunAsync_FalseDelegate_ReturnFalse()
        {
            //Arrange
            var task = new Task("task1", 2, 2, (x, y) => x + y == 5, TaskPriority.Middle, TaskType.Type1);

            //Act
            var result = await task.RunAsync();

            //Assert
            Assert.IsTrue(result.HasError());
        }

        [TestCase(1, TaskError.False)]
        [TestCase(0, TaskError.Exception)]
        public async System.Threading.Tasks.Task RunAsync_FalseDelegate_ReturnErrorType(int divider, TaskError expected)
        {
            //Arrange
            var task = new Task("task1", 2, divider, (x, y) => x / y == 5, TaskPriority.Middle, TaskType.Type1);

            //Act
            var result = await task.RunAsync();

            //Assert
            Assert.IsTrue(result.HasError());
            Assert.AreEqual(expected, result.Error.Value);
        }
    }
}
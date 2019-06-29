using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using TaskManagerLib.Enums;

namespace TaskManagerLib.Models
{
    public class TaskResult
    {
        public TaskError? Error;
        public string Message;

        private TaskResult([NotNull]TaskError error, [NotNull]string message)
        {
            Error = error;
            Message = message;
        }

        private TaskResult([NotNull]string message)
        {
            Message = message;
        }

        public bool HasError()
        {
            return Error != null;
        }

        public static TaskResult CreateSuccess(string message)
        {
            return new TaskResult(message);
        }

        public static TaskResult CreateError(TaskError error, string message)
        {
            return new TaskResult(error, message);
        }
    }
}
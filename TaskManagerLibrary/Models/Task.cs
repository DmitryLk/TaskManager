﻿using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Enums;

namespace TaskManager.Models
{
    internal class Task
    {
        /// <summary>
        /// Содержимое задачи
        /// </summary>
        public string Content { get; private set; }

        public TaskType Type { get; private set; }
        public TaskPriority Priority { get; private set; }

        public Task(string content, TaskPriority priority, TaskType type)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Priority = priority;
            Type = type;
        }

        public void UpdatePriority(TaskPriority priority)
        {
            this.Priority = priority;
        }

        //public string Content { get; private set; }
        //public TaskPriority Priority { get; private set; }
        //public TaskType Type { get; private set; }
    }
}
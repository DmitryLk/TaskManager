using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerLib.Enums
{
    /// <summary>
    /// Приоритеты задач
    /// </summary>
    public enum TaskPriority : byte
    {
        VeryLow = 1,
        Low = 2,
        Middle = 3,
        High = 4,
        VeryHigh = 5
    }
}
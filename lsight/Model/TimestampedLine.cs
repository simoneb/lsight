using System;
using lsight.Extensibility;

namespace lsight.Model
{
    public class TimestampedLine : ITimestampedLine
    {
        public DateTime Timestamp { get; set; }

        public string Contents { get; set; }
    }
}
using System;

namespace lsight.Extensibility
{
    public interface ITimestampedLine
    {
        DateTime Timestamp { get; set; }
        string Contents { get; set; }
    }
}
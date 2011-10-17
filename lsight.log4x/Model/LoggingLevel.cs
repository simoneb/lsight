using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace lsight.log4x.Model
{
    public class LoggingLevel
    {
        public static readonly LoggingLevel Debug = new LoggingLevel("DEBUG", 1);
        public static readonly LoggingLevel Info = new LoggingLevel("INFO", 2);
        public static readonly LoggingLevel Warn = new LoggingLevel("WARN", 3);
        public static readonly LoggingLevel Error = new LoggingLevel("ERROR", 4);
        public static readonly LoggingLevel Fatal = new LoggingLevel("FATAL", 5);

        private LoggingLevel(string name, int priority)
        {
            Name = name;
            Priority = priority;
        }

        public static readonly IEnumerable<LoggingLevel> All = new LoggingLevels(Debug, Info, Warn, Error, Fatal); 
        public static IEnumerable<string> AllNames { get { return All.Select(l => l.Name); }}

        public string Name { get; private set; }

        public static LoggingLevel Parse(string value)
        {
            return All.First(l => l.Name.Equals(value, StringComparison.Ordinal));
        }

        public static bool operator >=(LoggingLevel first, LoggingLevel second)
        {
            return first.Priority >= second.Priority;
        }

        public static bool operator <=(LoggingLevel first, LoggingLevel second)
        {
            return first.Priority <= second.Priority;
        }

        public static bool operator <(LoggingLevel first, LoggingLevel second)
        {
            return first.Priority < second.Priority;
        }

        public static bool operator >(LoggingLevel first, LoggingLevel second)
        {
            return first.Priority > second.Priority;
        }

        private int Priority { get; set; }

        public override string ToString()
        {
            return Name;
        }

        private class LoggingLevels : IEnumerable<LoggingLevel>
        {
            private readonly LoggingLevel[] levels;

            public LoggingLevels(params LoggingLevel[] levels)
            {
                this.levels = levels;
            }

            IEnumerator<LoggingLevel> IEnumerable<LoggingLevel>.GetEnumerator()
            {
                return levels.Select(l => l).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<LoggingLevel>)this).GetEnumerator();
            }
        }
    }
}
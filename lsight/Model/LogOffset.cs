using System;
using System.Globalization;

namespace lsight.Model
{
    public class LogOffset
    {
        private static readonly CultureInfo Culture = CultureInfo.CurrentCulture;
        public static readonly LogOffset Zero = new LogOffset(false, 0, 0, 0, 0);

        private LogOffset(bool negative, int hours, int minutes, int seconds, int milliseconds)
        {
            Negative = negative;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Milliseconds = milliseconds;
        }

        public TimeSpan Timespan
        {
            get { return TimeSpan.FromSeconds((Negative ? -1 : 1)*(Milliseconds/1000d + Seconds + Minutes*60 + Hours*3600)); }
        }

        protected int Milliseconds { get; private set; }

        protected int Seconds { get; private set; }

        protected int Minutes { get; private set; }

        protected int Hours { get; private set; }

        protected bool Negative { get; private set; }

        public override string ToString()
        {
            return string.Format(Culture, "{0} {1:00}{6}{2:00}{6}{3:00}{5}{4:000}", Negative ? "-" : "+", Hours, Minutes,
                                 Seconds, Milliseconds, Culture.NumberFormat.NumberDecimalSeparator,
                                 Culture.DateTimeFormat.TimeSeparator);
        }

        public static LogOffset Parse(string @string)
        {
            var tokens = @string.Split(new[]
            {
                Culture.NumberFormat.NumberDecimalSeparator,
                Culture.DateTimeFormat.TimeSeparator, " "
            }, StringSplitOptions.None);

            return new LogOffset(tokens[0].Equals("-"),
                                 int.Parse(tokens[1]),
                                 int.Parse(tokens[2]),
                                 int.Parse(tokens[3]),
                                 int.Parse(tokens[4]));
        }
    }
}
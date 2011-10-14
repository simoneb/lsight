using System;
using System.Windows.Media;
using Caliburn.Micro;
using lsight.Model;

namespace lsight.Logs.Lines
{
    internal class LogLineViewModel : PropertyChangedBase
    {
        private SolidColorBrush brush;
        private string contents;
        private LogOffset offset;

        public LogLineViewModel(TimestampedLine line, string path, LogOffset offset, Color color)
        {
            Contents = line.Line;
            Path = path;
            Timestamp = line.Timestamp;
            Offset = offset;
            Brush = new SolidColorBrush(color);
        }

        public string Contents
        {
            get { return contents; }
            set
            {
                contents = value;
                NotifyOfPropertyChange(() => Contents);
            }
        }

        public string Path { get; set; }
        public DateTime Timestamp { get; set; }

        public LogOffset Offset
        {
            get { return offset; }
            set
            {
                offset = value;
                NotifyOfPropertyChange(() => Offset);
            }
        }

        public SolidColorBrush Brush
        {
            get { return brush; }
            set
            {
                brush = value;
                NotifyOfPropertyChange(() => Brush);
            }
        }

        public DateTime TimestampIncludingOffset { get { return Timestamp.Add(Offset.Timespan); } }

        public void ChangeColor(Color color)
        {
            Brush = new SolidColorBrush(color);
        }

        public void ChangeHourOffset(LogOffset hourOffset)
        {
            Offset = hourOffset;
        }
    }
}
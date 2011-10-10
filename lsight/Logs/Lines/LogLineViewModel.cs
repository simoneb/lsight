using System;
using System.Windows.Media;
using Caliburn.Micro;

namespace lsight.Logs.Lines
{
    internal class LogLineViewModel : PropertyChangedBase
    {
        private SolidColorBrush brush;
        private string contents;
        private int hourOffset;

        public LogLineViewModel(string contents, string path, Color color, DateTime timestamp, int hourOffset)
        {
            Contents = contents;
            Path = path;
            Timestamp = timestamp;
            HourOffset = hourOffset;
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

        public int HourOffset
        {
            get { return hourOffset; }
            set
            {
                hourOffset = value;
                NotifyOfPropertyChange(() => HourOffset);
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

        public DateTime TimestampIncludingOffset { get { return Timestamp.AddHours(HourOffset); } }

        public void ChangeColor(Color color)
        {
            Brush = new SolidColorBrush(color);
        }

        public void ChangeHourOffset(int hourOffset)
        {
            HourOffset = hourOffset;
        }
    }
}
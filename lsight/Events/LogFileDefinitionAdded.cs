using System.Windows.Media;

namespace lsight.Events
{
    public class LogFileDefinitionAdded
    {
        public string Path { get; set; }
        public Color Color { get; set; }
        public string TimestampPattern { get; set; }
        public int HourOffset { get; set; }

        public LogFileDefinitionAdded(string path, Color color, string timestampPattern, int hourOffset)
        {
            Path = path;
            Color = color;
            TimestampPattern = timestampPattern;
            HourOffset = hourOffset;
        }
    }
}
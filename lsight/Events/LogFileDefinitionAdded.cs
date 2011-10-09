using System.Windows.Media;

namespace lsight.Events
{
    public class LogFileDefinitionAdded
    {
        public string Path { get; set; }
        public Color Color { get; set; }
        public string TimestampPattern { get; set; }

        public LogFileDefinitionAdded(string path, Color color, string timestampPattern)
        {
            Path = path;
            Color = color;
            TimestampPattern = timestampPattern;
        }
    }
}
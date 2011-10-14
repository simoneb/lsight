using System.Windows.Media;
using lsight.Model;

namespace lsight.Events
{
    public class LogFileDefinitionAdded
    {
        public string Path { get; private set; }
        public Color Color { get; private set; }
        public string TimestampPattern { get; private set; }
        public LogOffset Offset { get; private set; }

        public LogFileDefinitionAdded(string path, Color color, string timestampPattern, LogOffset offset)
        {
            Path = path;
            Color = color;
            TimestampPattern = timestampPattern;
            Offset = offset;
        }
    }
}
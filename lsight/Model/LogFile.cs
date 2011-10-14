using System.Windows.Media;

namespace lsight.Model
{
    public class LogFile
    {
        public string Path { get; set; }
        public Color Color { get; set; }
        public string TimestampPattern { get; set; }
        public LogOffset Offset { get; set; }

        public LogFile(string path, Color color, string timestampPattern, LogOffset offset)
        {
            Path = path;
            Color = color;
            TimestampPattern = timestampPattern;
            Offset = offset;
        }
    }
}
using System.Windows.Media;

namespace lsight.Commands
{
    internal class AddLogFileDefinitionCommand
    {
        public string Path { get; set; }
        public Color Color { get; set; }
        public string TimestampPattern { get; set; }
        public int HourOffset { get; set; }

        public AddLogFileDefinitionCommand(string path, Color color, string timestampPattern, int hourOffset)
        {
            Path = path;
            Color = color;
            TimestampPattern = timestampPattern;
            HourOffset = hourOffset;
        }
    }
}
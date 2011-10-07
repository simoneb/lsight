using System.Windows.Media;

namespace lsight.Commands
{
    internal class ChangeLogFileColorCommand
    {
        public string Path { get; set; }
        public Color Color { get; set; }

        public ChangeLogFileColorCommand(string path, Color color)
        {
            Path = path;
            Color = color;
        }
    }
}
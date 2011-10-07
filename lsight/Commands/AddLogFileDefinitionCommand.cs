using System.Windows.Media;

namespace lsight.Commands
{
    internal class AddLogFileDefinitionCommand
    {
        public string Path { get; set; }
        public Color Color { get; set; }

        public AddLogFileDefinitionCommand(string path, Color color)
        {
            Path = path;
            Color = color;
        }
    }
}
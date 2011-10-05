using System.Windows.Media;

namespace lsight.Settings.LogFileDefinition
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
using System.Windows.Media;

namespace lsight.Settings.LogFileDefinition
{
    internal interface ILogFileDefinition
    {
        string Path { get; set; }
        Color Color { get; set; }
    }
}
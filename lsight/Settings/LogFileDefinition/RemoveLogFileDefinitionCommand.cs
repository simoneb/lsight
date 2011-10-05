namespace lsight.Settings.LogFileDefinition
{
    internal class RemoveLogFileDefinitionCommand
    {
        public string Path { get; set; }

        public RemoveLogFileDefinitionCommand(string path)
        {
            Path = path;
        }
    }
}
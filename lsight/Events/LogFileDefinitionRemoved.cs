namespace lsight.Events
{
    internal class LogFileDefinitionRemoved
    {
        public string Path { get; set; }

        public LogFileDefinitionRemoved(string path)
        {
            Path = path;
        }
    }
}
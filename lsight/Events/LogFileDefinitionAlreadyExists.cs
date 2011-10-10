using System;

namespace lsight.Events
{
    public class LogFileDefinitionAlreadyExists
    {
        public string Path { get; set; }

        public LogFileDefinitionAlreadyExists(string path)
        {
            Path = path;
        }
    }
}
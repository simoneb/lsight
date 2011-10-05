namespace lsight.Settings.LogFileDefinition
{
    internal class RemoveLogFileDefinitionCommand
    {
        public ExistingLogFileDefinitionViewModel ExistingLog { get; set; }

        public RemoveLogFileDefinitionCommand(ExistingLogFileDefinitionViewModel existingLog)
        {
            ExistingLog = existingLog;
        }
    }
}
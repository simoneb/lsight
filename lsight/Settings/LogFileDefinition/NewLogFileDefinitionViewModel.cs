using Caliburn.Micro;

namespace lsight.Settings.LogFileDefinition
{
    public class NewLogFileDefinitionViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator aggregator;
        private LogFileDefinitionViewModel definition;

        public NewLogFileDefinitionViewModel(IEventAggregator aggregator)
        {
            this.aggregator = aggregator;
            Definition = new LogFileDefinitionViewModel();
        }

        public LogFileDefinitionViewModel Definition
        {
            get { return definition; }
            set
            {
                definition = value;
                NotifyOfPropertyChange(() => Definition);
            }
        }

        public void Add()
        {
            aggregator.Publish(new AddLogFileDefinitionCommand(definition.Path, definition.Color));
            Definition = new LogFileDefinitionViewModel();
        }
    }
}
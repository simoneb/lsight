using System.Windows.Media;
using Caliburn.Micro;

namespace lsight.Settings.LogFileDefinition
{
    class ExistingLogFileDefinitionViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator aggregator;
        private LogFileDefinitionViewModel definition;

        public ExistingLogFileDefinitionViewModel(string path, Color color, IEventAggregator aggregator)
        {
            this.aggregator = aggregator;
            Definition = new LogFileDefinitionViewModel {Color = color, Path = path};
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

        public void Remove()
        {
            aggregator.Publish(new RemoveLogFileDefinitionCommand(this));
        }
    }
}
using System.Windows.Media;
using Caliburn.Micro;

namespace lsight.Settings.LogFileDefinition
{
    class ExistingLogFileDefinitionViewModel : PropertyChangedBase, ILogFileDefinition
    {
        private readonly IEventAggregator aggregator;
        private ILogFileDefinition definition;

        public ExistingLogFileDefinitionViewModel(string path, Color color, IEventAggregator aggregator)
        {
            this.aggregator = aggregator;
            Definition = new LogFileDefinitionViewModel {Color = color, Path = path};
        }

        public ILogFileDefinition Definition
        {
            get { return definition; }
            set
            {
                definition = value;
                NotifyOfPropertyChange(() => Definition);
            }
        }

        public string Path
        {
            get { return definition.Path; }
            set { definition.Path = value; }
        }

        public Color Color
        {
            get { return definition.Color; }
            set { definition.Color = value; }
        }

        public void Remove()
        {
            aggregator.Publish(new RemoveLogFileDefinitionCommand(Path));
        }
    }
}
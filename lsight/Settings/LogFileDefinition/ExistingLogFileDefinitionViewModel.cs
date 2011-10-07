using System.Windows.Media;
using Caliburn.Micro;
using lsight.Commands;

namespace lsight.Settings.LogFileDefinition
{
    internal class ExistingLogFileDefinitionViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator aggregator;
        private string path;
        private Color color;
        private bool initializing;

        public ExistingLogFileDefinitionViewModel(string path, Color color, IEventAggregator aggregator)
        {
            initializing = true;
            this.aggregator = aggregator;
            Path = path;
            Color = color;
            initializing = false;
        }

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                NotifyOfPropertyChange(() => Path);
            }
        }

        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
                NotifyOfPropertyChange(() => Color);

                if(!initializing)
                    aggregator.Publish(new ChangeLogFileColorCommand(Path, Color));
            }
        }

        public void Remove()
        {
            aggregator.Publish(new RemoveLogFileDefinitionCommand(Path));
        }
    }
}
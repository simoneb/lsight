using System.Windows.Media;
using Caliburn.Micro;
using lsight.Commands;

namespace lsight.Settings.LogFileDefinition
{
    internal class ExistingLogFileDefinitionViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator aggregator;
        private readonly bool initializing;
        private Color color;
        private string path;
        private string timestampRegex;

        public ExistingLogFileDefinitionViewModel(string path, Color color, string timestampRegex,
                                                  IEventAggregator aggregator)
        {
            initializing = true;
            this.aggregator = aggregator;
            Path = path;
            Color = color;
            TimestampRegex = timestampRegex;
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

                if (!initializing)
                    aggregator.Publish(new ChangeLogFileColorCommand(Path, Color));
            }
        }

        public string TimestampRegex
        {
            get { return timestampRegex; }
            set
            {
                timestampRegex = value;
                NotifyOfPropertyChange(() => TimestampRegex);
            }
        }

        public void Remove()
        {
            aggregator.Publish(new RemoveLogFileDefinitionCommand(Path));
        }
    }
}
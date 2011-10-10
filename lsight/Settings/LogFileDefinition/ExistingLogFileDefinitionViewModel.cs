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
        private int hourOffset;
        private string path;
        private string timestampPattern;

        public ExistingLogFileDefinitionViewModel(string path, Color color, string timestampPattern, int hourOffset,
                                                  IEventAggregator aggregator)
        {
            initializing = true;
            this.aggregator = aggregator;
            Path = path;
            Color = color;
            TimestampPattern = timestampPattern;
            HourOffset = hourOffset;
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

        public string TimestampPattern
        {
            get { return timestampPattern; }
            set
            {
                timestampPattern = value;
                NotifyOfPropertyChange(() => TimestampPattern);
            }
        }

        public int HourOffset
        {
            get { return hourOffset; }
            set
            {
                hourOffset = value;
                NotifyOfPropertyChange(() => HourOffset);
            }
        }

        public void Remove()
        {
            aggregator.Publish(new RemoveLogFileDefinitionCommand(Path));
        }
    }
}
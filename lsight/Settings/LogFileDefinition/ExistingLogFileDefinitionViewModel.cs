using System.Windows.Media;
using Caliburn.Micro;
using lsight.Commands;
using lsight.Model;

namespace lsight.Settings.LogFileDefinition
{
    internal class ExistingLogFileDefinitionViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator aggregator;
        private readonly bool initializing;
        private Color color;
        private LogOffset offset;
        private string path;
        private string timestampPattern;

        public ExistingLogFileDefinitionViewModel(LogFile logFile, IEventAggregator aggregator)
        {
            initializing = true;
            this.aggregator = aggregator;
            Path = logFile.Path;
            Color = logFile.Color;
            TimestampPattern = logFile.TimestampPattern;
            Offset = logFile.Offset;
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

        public LogOffset Offset
        {
            get { return offset; }
            set
            {
                offset = value;
                NotifyOfPropertyChange(() => Offset);
            }
        }

        public void Remove()
        {
            aggregator.Publish(new RemoveLogFileDefinitionCommand(Path));
        }
    }
}
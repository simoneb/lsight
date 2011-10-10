using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using Caliburn.Micro;
using lsight.Commands;
using lsight.Events;
using System.Linq;
using lsight.Logs.Lines;
using lsight.Services;

namespace lsight.Logs
{
    [Export(typeof(ILogs))]
    class LogsViewModel : PropertyChangedBase, ILogs, IHandle<LogFileDefinitionAdded>, IHandle<LogFileDefinitionRemoved>, IHandle<ChangeLogFileColorCommand>
    {
        private readonly ITimestampingService timestampingService;
        private ObservableCollection<LogLineViewModel> lines = new ObservableCollection<LogLineViewModel>();

        [ImportingConstructor]
        public LogsViewModel(IEventAggregator aggregator, ITimestampingService timestampingService)
        {
            this.timestampingService = timestampingService;
            aggregator.Subscribe(this);
        }

        public void Handle(LogFileDefinitionAdded message)
        {
            var i = 0;
            var appendToBottom = false;
            
            foreach (var timestampedLine in timestampingService.Timestamp(File.ReadLines(message.Path), message.TimestampPattern).OrderBy(l => l.Timestamp))
            {
                var newLine = new LogLineViewModel(timestampedLine.Line, message.Path, message.Color, timestampedLine.Timestamp, message.HourOffset);

                if(appendToBottom)
                {
                    Lines.Add(newLine);
                    continue;
                }

                while (Lines.Count > i && Lines[i].TimestampIncludingOffset < newLine.TimestampIncludingOffset)
                    i++;

                if (Lines.Count > i)
                    Lines.Insert(i++, newLine);
                else /* reached bottom of list */
                {
                    Lines.Add(newLine);
                    appendToBottom = true;
                }
            }
        }

        public ObservableCollection<LogLineViewModel> Lines
        {
            get { return lines; }
            set
            {
                lines = value;
                NotifyOfPropertyChange(() => Lines);
            }
        }

        public void Handle(LogFileDefinitionRemoved message)
        {
            var linesToRemove = Lines.Where(line => line.Path.Equals(message.Path)).ToArray();

            foreach (var line in linesToRemove)
                Lines.Remove(line);
        }

        public void Handle(ChangeLogFileColorCommand message)
        {
            Lines.Where(l => l.Path.Equals(message.Path)).Apply(l => l.ChangeColor(message.Color));
        }
    }
}
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using Caliburn.Micro;
using lsight.Commands;
using lsight.Events;
using System.Linq;
using lsight.Logs.Lines;

namespace lsight.Logs
{
    [Export(typeof(ILogs))]
    class LogsViewModel : PropertyChangedBase, ILogs, IHandle<LogFileDefinitionAdded>, IHandle<LogFileDefinitionRemoved>, IHandle<ChangeLogFileColorCommand>
    {
        private ObservableCollection<LogLineViewModel> lines = new ObservableCollection<LogLineViewModel>();

        [ImportingConstructor]
        public LogsViewModel(IEventAggregator aggregator)
        {
            aggregator.Subscribe(this);
        }

        public void Handle(LogFileDefinitionAdded message)
        {
            (from line in File.ReadAllLines(message.Path)
             select new LogLineViewModel(line, message.Path, message.Color))
             .Apply(Lines.Add);
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
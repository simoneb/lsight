using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using Caliburn.Micro;
using lsight.Events;

namespace lsight.Logs
{
    [Export(typeof(ILogs))]
    class LogsViewModel : PropertyChangedBase, ILogs, IHandle<LogFileDefinitionAdded>, IHandle<LogFileDefinitionRemoved>
    {
        private ObservableCollection<string> lines;

        [ImportingConstructor]
        public LogsViewModel(IEventAggregator aggregator)
        {
            aggregator.Subscribe(this);
        }

        public void Handle(LogFileDefinitionAdded message)
        {
            Lines = new ObservableCollection<string>(File.ReadAllLines(message.Path));
        }

        public ObservableCollection<string> Lines
        {
            get {
                return lines;
            }
            set {
                lines = value;
                NotifyOfPropertyChange(() => Lines);
            }
        }

        public void Handle(LogFileDefinitionRemoved message)
        {
            Lines.Clear();
        }
    }
}
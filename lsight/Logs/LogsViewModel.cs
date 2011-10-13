using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows.Data;
using Caliburn.Micro;
using lsight.Commands;
using lsight.Events;
using System.Linq;
using lsight.Logs.Lines;
using lsight.Services;

namespace lsight.Logs
{
    [Export(typeof(ILogs))]
    class LogsViewModel : Screen, ILogs, IHandle<LogFileDefinitionAdded>, IHandle<LogFileDefinitionRemoved>, IHandle<ChangeLogFileColorCommand>
    {
        private readonly ITimestampingService timestampingService;
        private readonly ObservableCollection<LogLineViewModel> source = new ObservableCollection<LogLineViewModel>();
        private readonly CollectionViewSource viewSource = new CollectionViewSource();
        private ListCollectionView lines;

        [ImportingConstructor]
        public LogsViewModel(IEventAggregator aggregator, ITimestampingService timestampingService)
        {
            this.timestampingService = timestampingService;
            aggregator.Subscribe(this);

            viewSource.Source = source;
            Lines = (ListCollectionView)viewSource.View;
            Lines.CustomSort = new LogLineViewModelComparer();
        }

        public void Handle(LogFileDefinitionAdded message)
        {
            using(viewSource.DeferRefresh())
                foreach (var line in timestampingService.Timestamp(File.ReadLines(message.Path), message.TimestampPattern))
                    source.Add(new LogLineViewModel(line.Line, message.Path, message.Color, line.Timestamp, message.HourOffset));
        }

        public ListCollectionView Lines
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
            var linesToRemove = source.Where(line => line.Path.Equals(message.Path)).ToArray();

            using(viewSource.DeferRefresh())
                foreach (var line in linesToRemove)
                    source.Remove(line);
        }

        public void Handle(ChangeLogFileColorCommand message)
        {
            using (viewSource.DeferRefresh())
                source.Where(l => l.Path.Equals(message.Path)).Apply(l => l.ChangeColor(message.Color));
        }

        public void Export(string fileName)
        {
            File.WriteAllLines(fileName, Lines.Cast<LogLineViewModel>().Select(l => l.Contents));
        }
    }
}
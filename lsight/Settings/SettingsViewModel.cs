using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using lsight.Commands;
using lsight.Events;
using lsight.Settings.LogFileDefinition;
using System.Linq;

namespace lsight.Settings
{
    [Export(typeof (ISettings))]
    internal class SettingsViewModel : Screen, ISettings, IHandle<AddLogFileDefinitionCommand>, IHandle<RemoveLogFileDefinitionCommand>
    {
        [ImportingConstructor]
        public SettingsViewModel(IEventAggregator aggregator, INewLogFileDefinition newLogFileDefinition)
        {
            NewLogFileDefinition = newLogFileDefinition;
            LogDefinitions = new ObservableCollection<ExistingLogFileDefinitionViewModel>();

            this.aggregator = aggregator;

            aggregator.Subscribe(this);
        }

        private INewLogFileDefinition newLogDefinition;

        public INewLogFileDefinition NewLogFileDefinition
        {
            get { return newLogDefinition; }
            set
            {
                newLogDefinition = value;
                NotifyOfPropertyChange(() => NewLogFileDefinition);
            }
        }

        private ObservableCollection<ExistingLogFileDefinitionViewModel> logDefinitions;
        private readonly IEventAggregator aggregator;

        public ObservableCollection<ExistingLogFileDefinitionViewModel> LogDefinitions
        {
            get { return logDefinitions; }
            set
            {
                logDefinitions = value;
                NotifyOfPropertyChange(() => LogDefinitions);
            }
        }

        public void Handle(AddLogFileDefinitionCommand message)
        {
            if(LogDefinitions.Any(d => d.Path.Equals(message.Path, StringComparison.OrdinalIgnoreCase)))
            {
                aggregator.Publish(new LogFileDefinitionAlreadyExists(message.Path));
                return;
            }

            LogDefinitions.Add(new ExistingLogFileDefinitionViewModel(message.Path, message.Color, message.TimestampPattern, message.HourOffset, aggregator));
            aggregator.Publish(new LogFileDefinitionAdded(message.Path, message.Color, message.TimestampPattern, message.HourOffset));
        }

        public void Handle(RemoveLogFileDefinitionCommand message)
        {
            LogDefinitions.Remove(LogDefinitions.Single(d => d.Path.Equals(message.Path)));
            aggregator.Publish(new LogFileDefinitionRemoved(message.Path));
        }
    }
}
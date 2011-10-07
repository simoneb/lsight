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
        public SettingsViewModel(IEventAggregator aggregator)
        {
            NewLogFileDefinition = new NewLogFileDefinitionViewModel(aggregator);
            LogDefinitions = new ObservableCollection<ExistingLogFileDefinitionViewModel>();

            this.aggregator = aggregator;

            aggregator.Subscribe(this);
        }

        private NewLogFileDefinitionViewModel newLogDefinition;

        public NewLogFileDefinitionViewModel NewLogFileDefinition
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
            LogDefinitions.Add(new ExistingLogFileDefinitionViewModel(message.Path, message.Color, aggregator));
            aggregator.Publish(new LogFileDefinitionAdded(message.Path, message.Color));
        }

        public void Handle(RemoveLogFileDefinitionCommand message)
        {
            LogDefinitions.Remove(LogDefinitions.Single(d => d.Path.Equals(message.Path)));
            aggregator.Publish(new LogFileDefinitionRemoved(message.Path));
        }
    }
}
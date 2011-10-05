using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using lsight.Settings.LogFileDefinition;

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
        }

        public void Handle(RemoveLogFileDefinitionCommand message)
        {
            LogDefinitions.Remove(message.ExistingLog);
        }
    }
}
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using lsight.Settings.LogFileDefinition;

namespace lsight.Settings
{
    [Export(typeof (ISettings))]
    internal class SettingsViewModel : Screen, ISettings
    {
        public SettingsViewModel()
        {
            NewLogDefinition = new LogFileDefinitionViewModel();

            LogFiles = new ObservableCollection<ILogFileDefinition>();
        }

        private ILogFileDefinition newLogDefinition;

        public ILogFileDefinition NewLogDefinition
        {
            get { return newLogDefinition; }
            set
            {
                newLogDefinition = value;
                NotifyOfPropertyChange(() => NewLogDefinition);
            }
        }

        private ObservableCollection<ILogFileDefinition> logFiles;

        public ObservableCollection<ILogFileDefinition> LogFiles
        {
            get { return logFiles; }
            set
            {
                logFiles = value;
                NotifyOfPropertyChange(() => LogFiles);
            }
        }

        public void Add()
        {
            LogFiles.Add(NewLogDefinition);
            NewLogDefinition = new LogFileDefinitionViewModel();
        }
    }
}
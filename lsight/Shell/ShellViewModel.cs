using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using Caliburn.Micro;
using lsight.Events;
using lsight.Extensibility;
using lsight.Logs;
using lsight.Notification;
using lsight.Settings;
using Microsoft.Win32;

namespace lsight.Shell
{
    [Export(typeof(IShell))]
    class ShellViewModel : PropertyChangedBase, IShell, IHaveDisplayName, IHandle<LogFileDefinitionAlreadyExists>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IWindowManager windowManager;

        [ImportingConstructor]
        public ShellViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, [ImportMany] IEnumerable<IAddinSettingsViewModel> addins)
        {
            this.eventAggregator = eventAggregator;
            this.windowManager = windowManager;
            Addins = new ObservableCollection<IAddinSettingsViewModel>(addins);
            eventAggregator.Subscribe(this);
        }

        [Import]
        public ILogs Logs { get; set; }

        [Import]
        public ISettings Settings { get; set; }
        
        public void ShowAddinSettings(IAddinSettingsViewModel model)
        {
            windowManager.ShowDialog(model);
        }

        public void Export()
        {
            var d = new SaveFileDialog
            {
                DefaultExt = ".txt",
                AddExtension = true,
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            };
            
            if (d.ShowDialog() == true)
                Logs.Export(d.FileName);
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        private ObservableCollection<IAddinSettingsViewModel> addins;

        public ObservableCollection<IAddinSettingsViewModel> Addins
        {
            get { return addins; }
            set
            {
                addins = value;
                NotifyOfPropertyChange(() => Addins);
            }
        }

        public string DisplayName
        {
            get
            {
                return "lSight";
            }
            set {}
        }

        public void Handle(LogFileDefinitionAlreadyExists message)
        {
            windowManager.ShowPopup(new NotificationViewModel("Log file already exists"));
        }
    }
}
using System;
using System.ComponentModel.Composition;
using System.Windows;
using Caliburn.Micro;
using lsight.Events;
using lsight.Logs;
using lsight.Notification;
using lsight.Settings;

namespace lsight.Shell
{
    [Export(typeof(IShell))]
    class ShellViewModel : IShell, IHaveDisplayName, IHandle<LogFileDefinitionAlreadyExists>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IWindowManager windowManager;

        [ImportingConstructor]
        public ShellViewModel(IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            this.eventAggregator = eventAggregator;
            this.windowManager = windowManager;

            eventAggregator.Subscribe(this);
        }

        [Import]
        public ILogs Logs { get; set; }

        [Import]
        public ISettings Settings { get; set; }
        
        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public string DisplayName
        {
            get
            {
                return "lSight";
            }
            set
            {
            }
        }

        public void Handle(LogFileDefinitionAlreadyExists message)
        {
            windowManager.ShowPopup(new NotificationViewModel("Log file already exists"));
        }
    }
}
using System.ComponentModel.Composition;
using System.Windows;
using Caliburn.Micro;
using lsight.Logs;
using lsight.Settings;

namespace lsight.Shell
{
    [Export(typeof(IShell))]
    class ShellViewModel : IShell, IHaveDisplayName
    {
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
    }
}
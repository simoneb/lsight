using System.ComponentModel.Composition;
using System.Windows;
using Caliburn.Micro;
using lsight.Logs;
using lsight.Settings;

namespace lsight.Shell
{
    [Export(typeof(IShell))]
    class ShellViewModel : Conductor<object>.Collection.AllActive, IShell
    {
        [Import]
        public ILogs Logs { get; set; }

        [Import]
        public ISettings Settings { get; set; }
        
        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
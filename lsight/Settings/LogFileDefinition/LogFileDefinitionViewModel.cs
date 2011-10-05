using System.Windows.Media;
using Caliburn.Micro;
using Microsoft.Win32;

namespace lsight.Settings.LogFileDefinition
{
    public class LogFileDefinitionViewModel : PropertyChangedBase, ILogFileDefinition
    {
        private string path;

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                NotifyOfPropertyChange(() => Path);
            }
        }

        private Color color;

        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
                NotifyOfPropertyChange(() => Color);
            }
        }

        public void Browse()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false
            };

            if (dialog.ShowDialog() == true)
                Path = dialog.FileName;
        }
    }
}
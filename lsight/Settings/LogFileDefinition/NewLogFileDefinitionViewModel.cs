using System.Windows.Media;
using Caliburn.Micro;
using lsight.Commands;
using Microsoft.Win32;

namespace lsight.Settings.LogFileDefinition
{
    public class NewLogFileDefinitionViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator aggregator;

        public NewLogFileDefinitionViewModel(IEventAggregator aggregator)
        {
            this.aggregator = aggregator;
        }

        private string path;

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                NotifyOfPropertyChange(() => Path);
                NotifyOfPropertyChange(() => CanAdd);
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
                NotifyOfPropertyChange(() => CanAdd);
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

        public bool CanAdd
        {
            get { return !string.IsNullOrEmpty(Path) && Color != new Color(); }
        }

        public void Add()
        {
            aggregator.Publish(new AddLogFileDefinitionCommand(Path, Color));
            Path = string.Empty;
            Color = new Color();
        }
    }
}
using System.ComponentModel.Composition;
using System.Windows.Media;
using Caliburn.Micro;
using lsight.Commands;
using lsight.Events;
using lsight.Services;
using lsight.Settings.Preview;
using Microsoft.Win32;

namespace lsight.Settings.LogFileDefinition
{
    [Export(typeof(INewLogFileDefinition))]
    public class NewLogFileDefinitionViewModel : PropertyChangedBase, IHandle<LogFileDefinitionAdded>, INewLogFileDefinition
    {
        private readonly IEventAggregator aggregator;
        private readonly IWindowManager windowManager;
        private Color color;

        private string path;
        private string timestampRegex;
        private ITimestampingService timestampingService;

        [ImportingConstructor]
        public NewLogFileDefinitionViewModel(IEventAggregator aggregator, IWindowManager windowManager, ITimestampingService timestampingService)
        {
            this.aggregator = aggregator;
            this.windowManager = windowManager;
            this.timestampingService = timestampingService;

            aggregator.Subscribe(this);
        }

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                NotifyOfPropertyChange(() => Path);
                NotifyOfPropertyChange(() => CanAdd);
                NotifyOfPropertyChange(() => CanPreview);
            }
        }

        public string TimestampRegex
        {
            get { return timestampRegex; }
            set
            {
                timestampRegex = value;
                NotifyOfPropertyChange(() => TimestampRegex);
                NotifyOfPropertyChange(() => CanAdd);
                NotifyOfPropertyChange(() => CanPreview);
            }
        }

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

        public bool CanAdd
        {
            get { return !string.IsNullOrEmpty(Path) && !string.IsNullOrEmpty(TimestampRegex) && Color != new Color(); }
        }

        public bool CanPreview
        {
            get { return !string.IsNullOrEmpty(Path) && !string.IsNullOrEmpty(TimestampRegex); }
        }

        public void Preview()
        {
            windowManager.ShowDialog(new PreviewLogFileViewModel(Path, TimestampRegex, timestampingService));
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

        public void Add()
        {
            aggregator.Publish(new AddLogFileDefinitionCommand(Path, Color, TimestampRegex));
        }

        public void Handle(LogFileDefinitionAdded message)
        {
            Path = string.Empty;
            Color = new Color();
        }
    }
}
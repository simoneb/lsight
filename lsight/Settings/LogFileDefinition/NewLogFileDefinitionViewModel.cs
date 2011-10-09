using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Media;
using Caliburn.Micro;
using lsight.Commands;
using lsight.Events;
using lsight.Services;
using lsight.Settings.Preview;
using Microsoft.Win32;
using System.Linq;

namespace lsight.Settings.LogFileDefinition
{
    [Export(typeof(INewLogFileDefinition))]
    public class NewLogFileDefinitionViewModel : PropertyChangedBase, IHandle<LogFileDefinitionAdded>, INewLogFileDefinition
    {
        private readonly IEventAggregator aggregator;
        private readonly IWindowManager windowManager;
        private Color color;

        private string path;
        private string timestampPattern;
        private readonly ITimestampingService timestampingService;
        private readonly ISettingsStorage settingsStorage;
        private ObservableCollection<string> timestampPatterns;

        [ImportingConstructor]
        public NewLogFileDefinitionViewModel(IEventAggregator aggregator,
                                             IWindowManager windowManager,
                                             ITimestampingService timestampingService,
                                             ISettingsStorage settingsStorage)
        {
            this.aggregator = aggregator;
            this.windowManager = windowManager;
            this.timestampingService = timestampingService;
            this.settingsStorage = settingsStorage;

            timestampPatterns = new ObservableCollection<string>(settingsStorage.RecentTimestampPatterns);

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

        public string TimestampPattern
        {
            get { return timestampPattern; }
            set
            {
                timestampPattern = value;
                NotifyOfPropertyChange(() => TimestampPattern);
                NotifyOfPropertyChange(() => CanAdd);
                NotifyOfPropertyChange(() => CanPreview);
            }
        }

        public void EditTimestampPattern(string text)
        {
            TimestampPattern = text;
        }

        public void RemoveTimestampPattern(string pattern)
        {
            TimestampPatterns.Remove(pattern);
            settingsStorage.SetRecentTimestampPatterns(TimestampPatterns);
        }

        public ObservableCollection<string> TimestampPatterns
        {
            get { return timestampPatterns; }
            set
            {
                timestampPatterns = value;
                NotifyOfPropertyChange(() => TimestampPatterns);
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
            get { return !string.IsNullOrEmpty(Path) && !string.IsNullOrEmpty(TimestampPattern) && Color != new Color(); }
        }

        public bool CanPreview
        {
            get { return !string.IsNullOrEmpty(Path) && !string.IsNullOrEmpty(TimestampPattern); }
        }

        public void Preview()
        {
            windowManager.ShowPopup(new PreviewLogFileViewModel(Path, TimestampPattern, timestampingService));
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
            aggregator.Publish(new AddLogFileDefinitionCommand(Path, Color, TimestampPattern));

            RememberTimestampPattern();
        }

        private void RememberTimestampPattern()
        {
            if(!TimestampPatterns.Contains(TimestampPattern))
            {
                TimestampPatterns.Add(TimestampPattern);
                settingsStorage.SetRecentTimestampPatterns(TimestampPatterns);
            }
        }

        public void Handle(LogFileDefinitionAdded message)
        {
            Path = string.Empty;
            Color = new Color();
        }
    }
}
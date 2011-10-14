using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Media;
using Caliburn.Micro;
using lsight.Commands;
using lsight.Events;
using lsight.Model;
using lsight.Services;
using lsight.Settings.Preview;
using Microsoft.Win32;

namespace lsight.Settings.LogFileDefinition
{
    [Export(typeof (INewLogFileDefinition))]
    public class NewLogFileDefinitionViewModel : PropertyChangedBase, IHandle<LogFileDefinitionAdded>,
                                                 INewLogFileDefinition
    {
        private readonly IEventAggregator aggregator;
        private readonly ISettingsStorage settingsStorage;
        private readonly ITimestampingService timestampingService;
        private readonly IWindowManager windowManager;
        private Color color;
        private LogOffset offset;

        private string path;
        private string timestampPattern;
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
            Offset = LogOffset.Zero;
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

        public ObservableCollection<string> TimestampPatterns
        {
            get { return timestampPatterns; }
            set
            {
                timestampPatterns = value;
                NotifyOfPropertyChange(() => TimestampPatterns);
            }
        }

        public LogOffset Offset
        {
            get { return offset; }
            set
            {
                offset = value;
                NotifyOfPropertyChange(() => Offset);
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

        public void Handle(LogFileDefinitionAdded message)
        {
            Path = string.Empty;
            Color = new Color();
            Offset = LogOffset.Zero;
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
            aggregator.Publish(new AddLogFileDefinitionCommand(Path, Color, TimestampPattern, Offset));

            RememberTimestampPattern();
        }

        private void RememberTimestampPattern()
        {
            if (!TimestampPatterns.Contains(TimestampPattern))
            {
                TimestampPatterns.Add(TimestampPattern);
                settingsStorage.SetRecentTimestampPatterns(TimestampPatterns);
            }
        }
    }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Media;
using Caliburn.Micro;
using lsight.Extensibility;

namespace lsight.log4x
{
    [Export(typeof(IAddinSettingsViewModel))]
    public class SettingsViewModel : PropertyChangedBase, IAddinSettingsViewModel
    {
        private readonly IEnumerable<string> loggingLevels = new[] {"DEBUG", "INFO", "WARN", "ERROR", "FATAL"};
        private ObservableCollection<LevelColorViewModel> levelColors;

        private ObservableCollection<string> levelFilter;

        private string selectedLevelFilter;

        public SettingsViewModel()
        {
            LevelColors = new ObservableCollection<LevelColorViewModel>(from level in loggingLevels
                                                                        select new LevelColorViewModel {LevelName = level, Color = new Color()});
            LevelFilter = new ObservableCollection<string>(loggingLevels);
            SelectedLevelFilter = LevelFilter.First();
        }

        public ObservableCollection<LevelColorViewModel> LevelColors
        {
            get { return levelColors; }
            set
            {
                levelColors = value;
                NotifyOfPropertyChange(() => LevelColors);
            }
        }

        public ObservableCollection<string> LevelFilter
        {
            get { return levelFilter; }
            set
            {
                levelFilter = value;
                NotifyOfPropertyChange(() => LevelFilter);
            }
        }

        public string SelectedLevelFilter
        {
            get { return selectedLevelFilter; }
            set
            {
                selectedLevelFilter = value;
                NotifyOfPropertyChange(() => SelectedLevelFilter);
            }
        }

        public string DisplayName
        {
            get { return "log4x"; }
            set {  }
        }
    }
}
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using lsight.Extensibility;
using lsight.log4x.Model;

namespace lsight.log4x
{
    [Export(typeof(IAddinSettingsViewModel))]
    public class SettingsViewModel : PropertyChangedBase, IAddinSettingsViewModel
    {
        private readonly ISettings settings;

        private ObservableCollection<LevelColorViewModel> levelColors;

        private ObservableCollection<string> levelFilter;

        private string selectedLevelFilter;

        [ImportingConstructor]
        public SettingsViewModel(ISettings settings)
        {
            this.settings = settings;

            LevelColors = new ObservableCollection<LevelColorViewModel>(from c in settings.Colors
                                                                        select new LevelColorViewModel {LevelName = c.Key.Name, Color = c.Value});
            LevelFilter = new ObservableCollection<string>(LoggingLevel.AllNames);
            SelectedLevelFilter = settings.Threshold.Name;
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
                settings.ChangeThreshold(LoggingLevel.Parse(value));
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
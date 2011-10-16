using System.Windows.Media;
using Caliburn.Micro;

namespace lsight.log4x
{
    public class LevelColorViewModel : PropertyChangedBase
    {
        private string levelName;
        public string LevelName
        {
            get { return levelName; }
            set
            {
                levelName = value;
                NotifyOfPropertyChange(() => LevelName);
            }
        }

        public Color Color { get; set; }
    }
}
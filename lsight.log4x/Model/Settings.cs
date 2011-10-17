using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Media;

namespace lsight.log4x.Model
{
    [Export(typeof(ISettings))]
    class Settings : ISettings
    {
        public Settings()
        {
            Threshold = LoggingLevel.Debug;
            Colors = LoggingLevel.All.ToDictionary(l => l, level => new Color());
        }

        public bool ShowUnmatchedLines
        {
            get { return true; }
        }

        public LoggingLevel Threshold
        {
            get; private set;
        }

        public IDictionary<LoggingLevel, Color> Colors { get; private set; }

        public void ChangeThreshold(LoggingLevel threshold)
        {
            Threshold = threshold;
        }
    }
}
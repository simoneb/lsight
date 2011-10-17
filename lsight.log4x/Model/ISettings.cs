using System.Collections.Generic;
using System.Windows.Media;

namespace lsight.log4x.Model
{
    public interface ISettings
    {
        bool ShowUnmatchedLines { get; }
        LoggingLevel Threshold { get; }
        IDictionary<LoggingLevel, Color> Colors { get; }
        void ChangeThreshold(LoggingLevel threshold);
    }
}
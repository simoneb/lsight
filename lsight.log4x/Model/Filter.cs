using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using lsight.Extensibility;

namespace lsight.log4x.Model
{
    [Export(typeof(IFilterAddin))]
    public class Filter : IFilterAddin
    {
        private readonly ISettings settings;
        private static readonly Regex Regex = new Regex(string.Format(@"(?<level>{0})", string.Join("|", LoggingLevel.All)));

        [ImportingConstructor]
        public Filter(ISettings settings)
        {
            this.settings = settings;
        }

        public bool Allow(ITimestampedLine timestampedLine)
        {
            var match = Regex.Match(timestampedLine.Contents);

            if (!match.Success)
                return settings.ShowUnmatchedLines;

            var level = LoggingLevel.Parse(match.Groups["level"].Value);

            return level >= settings.Threshold;
        }
    }
}
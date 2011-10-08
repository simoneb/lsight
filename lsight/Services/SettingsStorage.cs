using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace lsight.Services
{
    [Export(typeof(ISettingsStorage))]
    class SettingsStorage : ISettingsStorage
    {
        public IEnumerable<string> RecentTimestampPatterns
        {
            get { return Properties.Settings.Default.RecentTimestampPatterns.Cast<string>(); }
        }

        public void SetRecentTimestampPatterns(IEnumerable<string> patterns)
        {
            Properties.Settings.Default.RecentTimestampPatterns.Clear();
            Properties.Settings.Default.RecentTimestampPatterns.AddRange(patterns.ToArray());
        }

        public void Persist()
        {
            Properties.Settings.Default.Save();
        }
    }
}
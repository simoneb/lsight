using System.Collections.Generic;

namespace lsight.Services
{
    public interface ISettingsStorage
    {
        IEnumerable<string> RecentTimestampPatterns { get; }
        void Persist();
        void SetRecentTimestampPatterns(IEnumerable<string> patterns);
    }
}
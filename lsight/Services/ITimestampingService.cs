using System.Collections.Generic;
using lsight.Model;

namespace lsight.Services
{
    public interface ITimestampingService
    {
        IEnumerable<TimestampedLine> Timestamp(IEnumerable<string> lines, string timestampPattern);
    }
}
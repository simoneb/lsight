using System.Collections;
using lsight.Logs.Lines;

namespace lsight.Logs
{
    internal class LogLineViewModelComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((LogLineViewModel) x).TimestampIncludingOffset.CompareTo(((LogLineViewModel) y).TimestampIncludingOffset);
        }
    }
}
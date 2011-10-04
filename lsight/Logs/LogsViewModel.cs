using System.ComponentModel.Composition;

namespace lsight.Logs
{
    [Export(typeof(ILogs))]
    class LogsViewModel : ILogs
    {
    }
}
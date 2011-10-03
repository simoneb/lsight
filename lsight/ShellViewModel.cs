using System.ComponentModel.Composition;

namespace lsight
{
    [Export(typeof(IShell))]
    class ShellViewModel : IShell
    {
    }
}
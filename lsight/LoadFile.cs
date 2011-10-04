using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using Caliburn.Micro;
using lsight.Shell;
using Microsoft.Win32;

namespace lsight
{
    internal class LoadFile : IResult
    {
        [Import]
        public IShell Shell { private get; set; }

        public void Execute(ActionExecutionContext context)
        {
            var dialog = new OpenFileDialog
                         {
                             CheckFileExists = true,
                             CheckPathExists = true,
                             Multiselect = false
                         };

            if(dialog.ShowDialog() == true)
            {
                //Shell.Lines = new ObservableCollection<string>(File.ReadAllLines(dialog.FileName));
                Completed(this, new ResultCompletionEventArgs());
            }
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;
    }
}
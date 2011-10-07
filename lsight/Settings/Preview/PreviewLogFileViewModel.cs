using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Caliburn.Micro;
using lsight.Model;
using lsight.Services;

namespace lsight.Settings.Preview
{
    public class PreviewLogFileViewModel : Screen
    {
        private readonly string path;
        private readonly string timestampPattern;
        private readonly ITimestampingService timestampingService;
        private ObservableCollection<TimestampedLine> lines;

        public PreviewLogFileViewModel(string path, string timestampPattern, ITimestampingService timestampingService)
        {
            this.path = path;
            this.timestampPattern = timestampPattern;
            this.timestampingService = timestampingService;
        }

        public ObservableCollection<TimestampedLine> Lines
        {
            get { return lines; }
            set
            {
                lines = value;
                NotifyOfPropertyChange(() => Lines);
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            var timestampedLines = timestampingService.Timestamp(File.ReadLines(path).Take(10), timestampPattern);

            Lines = new ObservableCollection<TimestampedLine>(timestampedLines.OrderBy(l => l.Timestamp));
        }
    }
}
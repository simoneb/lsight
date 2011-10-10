using Caliburn.Micro;

namespace lsight.Notification
{
    public class NotificationViewModel : Screen
    {
        private string message;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public NotificationViewModel(string message)
        {
            Message = message;
        }
    }
}
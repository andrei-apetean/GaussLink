using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;

namespace GaussLink.ViewModels.MainDisplay
{
    public class DisplayViewModel:BaseViewModel
    {
        DisplayVM displayVM = new OpenFileDisplay();

        public DisplayViewModel()
        {
            DisplayVM = new OpenFileDisplay();
            Messenger.Default.Register<FileMessage>(this, OnFileStackChanged);
        }

        private void OnFileStackChanged(FileMessage obj)
        {
            switch (obj.Message)
            {
                case "OpenFile": DisplayVM = new OpenFileDisplay();
                    break;
                case "TabDisplay": DisplayVM = new TabDisplayViewModel();
                    break;
            }

        }

        public DisplayVM DisplayVM
        {
            get
            {
                return displayVM;
            }
            set
            {
                displayVM = value;
                OnPropertyChanged(nameof(DisplayVM));
            }
        }
    }
}

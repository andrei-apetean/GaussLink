using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using GaussLink.ViewModels.Themes;
using Microsoft.Expression.Interactivity.Core;
using System;
using System.Windows.Input;

namespace GaussLink.ViewModels
{
    public class ExplorerItem:BaseViewModel
    {
        public ExplorerItem(string Name)
        {
            this.Name = Name;
            DeleteCommand = new ActionCommand(p => DeleteRequest?.Invoke(this, EventArgs.Empty));
            Messenger.Default.Register<ThemeChangedMessage>(this, OnThemeChanged);
            OnThemeChanged(new ThemeChangedMessage(ThemesController.CurrentTheme));
        }

        private void OnThemeChanged(ThemeChangedMessage obj)
        {
            switch (obj.ThemeType)
            {
                case ThemeType.Dark:
                    JobFileIcon = "/UI/Images/jobFileIconWhite.png";
                    BinIcon = "/UI/Images/deleteFileIconWhite.png";

                    break;
                case ThemeType.ColourfulDark:
                    JobFileIcon = "/UI/Images/jobFileIconWhite.png";
                    BinIcon = "/UI/Images/deleteFileIconWhite.png";

                    break;
                case ThemeType.Light:
                    JobFileIcon = "/UI/Images/jobFileIconBlack.png";
                    BinIcon = "/UI/Images/deleteFileIconBlack.png";

                    break;
                case ThemeType.ColourfulLight:
                    BinIcon = "/UI/Images/deleteFileIconBlack.png";
                    JobFileIcon = "/UI/Images/jobFileIconBlack.png";
                    break;
            }
        }

        private string jobFileIcon;
        public string JobFileIcon
        {
            get { return jobFileIcon; }
            set
            {
                jobFileIcon = value;
                OnPropertyChanged(nameof(JobFileIcon));
            }
        }

        private string binIcon;
        public string BinIcon
        {
            get { return binIcon; }
            set
            {
                binIcon = value;
                OnPropertyChanged(nameof(BinIcon));
            }
        }


        public string Name { get; set; }

        public ICommand DeleteCommand { get; }
        public event EventHandler DeleteRequest;
    }
}

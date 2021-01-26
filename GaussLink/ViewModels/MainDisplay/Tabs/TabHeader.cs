using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Events;
using GaussLink.Data.Messages;
using GaussLink.ViewModels.Themes;
using Microsoft.Expression.Interactivity.Core;
using System;
using System.Windows.Input;

namespace GaussLink.ViewModels.MainDisplay.Tabs
{
    public class TabHeader:BaseViewModel
    {
        public TabHeader()
        {
            CloseCommand = new ActionCommand(p => ClosedRequest?.Invoke(this, EventArgs.Empty));
            Messenger.Default.Register<ThemeChangedMessage>(this, OnThemeChanged);
            OnThemeChanged(new ThemeChangedMessage(ThemesController.CurrentTheme));
        }
        public TabHeader(string Name, double Width, TabContent TabContent)
        {
            this.Name = Name;
            this.Width = Width;
            this.TabContent = TabContent;
            CloseCommand = new ActionCommand(p => ClosedRequest?.Invoke(this, EventArgs.Empty));
            Messenger.Default.Register<ThemeChangedMessage>(this, OnThemeChanged);
            OnThemeChanged(new ThemeChangedMessage(ThemesController.CurrentTheme));
        }
        private double width;
        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
        public string Name { get; set; }
        public TabContent TabContent { get; set; }

        public ICommand CloseCommand { get; }
        public event EventHandler ClosedRequest;
        public void OnSizeChanged(object sender, SizeChangedArgs e)
        {
            this.Width = e.Width;
        }


        #region Icons
        private void OnThemeChanged(ThemeChangedMessage obj)
        {
            switch (obj.ThemeType)
            {
                case ThemeType.Dark:
                    CloseIcon = "/UI/Images/closeIconWhite.png";
                    break;
                case ThemeType.ColourfulDark:
                    CloseIcon = "/UI/Images/closeIconWhite.png";
                    break;
                case ThemeType.Light:
                    CloseIcon = "/UI/Images/closeIconBlack.png";
                    break;
                case ThemeType.ColourfulLight:
                    CloseIcon = "/UI/Images/closeIconBlack.png";
                    break;
            }
        }

        private string closeIcon;
        public string CloseIcon
        {
            get { return closeIcon; }
            set
            {
                closeIcon = value;
                OnPropertyChanged(nameof(CloseIcon));
            }
        }
        #endregion


    }
}


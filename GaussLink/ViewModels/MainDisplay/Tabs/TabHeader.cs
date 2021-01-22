using GaussLink.Data.Events;
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

        }
        public TabHeader(string Name, double Width, TabContent TabContent)
        {
            this.Name = Name;
            this.Width = Width;
            this.TabContent = TabContent;
            CloseCommand = new ActionCommand(p => ClosedRequest?.Invoke(this, EventArgs.Empty));

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
    }
}


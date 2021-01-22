using Microsoft.Expression.Interactivity.Core;
using System;
using System.Windows.Input;

namespace GaussLink.ViewModels
{
    public class ExplorerItem
    {
        public ExplorerItem(string Name)
        {
            this.Name = Name;
            DeleteCommand = new ActionCommand(p => DeleteRequest?.Invoke(this, EventArgs.Empty));

        }
    
        public string Name { get; set; }

        public ICommand DeleteCommand { get; }
        public event EventHandler DeleteRequest;
    }
}



using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using GaussLink.Models;
using GaussLink.ViewModels.Base;
using System.Windows.Input;

namespace GaussLink.ViewModels.MainDisplay.Tabs
{
    public class Viewer3DTab : TabContent
    {
        public Molecule3D Molecule3D { get; set; }
        public Viewer3DTab(Molecule3D Molecule3D)
        {
            this.Molecule3D = Molecule3D;
        }

        public Viewer3DTab()
        {

        }

        public ICommand InitializeViewModelCommand => new RelayCommand(InitializeViewModel);
        public void InitializeViewModel()
        {
            Messenger.Default.Send(new Viewer3DMessage(Molecule3D));
        }
    }
}

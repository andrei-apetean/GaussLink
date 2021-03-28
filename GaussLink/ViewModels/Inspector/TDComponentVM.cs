
using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data;
using GaussLink.Data.DataAccess;
using GaussLink.Data.Messages;
using GaussLink.Data.Store;
using GaussLink.Models;
using GaussLink.ViewModels.Base;
using GaussLink.ViewModels.Themes;
using GaussLink.Views.Windows.FileSaver;
using GaussLink.Views.Windows.Graph;
using System.Windows.Input;

namespace GaussLink.ViewModels
{
    public class TDComponentVM : ComponentVM
    {
        public TDComponentVM()
        {
        }
        public ICommand SaveCommand => new RelayCommand(Save);

        public virtual void Save()
        {
            //jobFile = DataManager.SelectedJobFile;
            DataManager.JobsToBeSaved.Add(DataManager.SelectedJobFile);
            FileSaverWindow fs = new FileSaverWindow();
            fs.Show();
        }
        public ICommand GetExcitationEnergiesCommand => new RelayCommand(GetExcitationEnergies);

        public virtual void GetExcitationEnergies()
        {
            Messenger.Default.Send(new DataMessage("Excitation Energy", DataManager.SelectedJobFile));
        }
        public ICommand UvVisSpecCommand => new RelayCommand(UvVisSpectrum);

        public virtual void UvVisSpectrum()
        {
            jobFile = DataManager.SelectedJobFile;
            ExcitationEnergy excitationEnergy = Extractor.ExtractExcitationEnergies(jobFile);
            UvVisGraphWindow gw = new UvVisGraphWindow(excitationEnergy);
            gw.Show();
        }

    }
}

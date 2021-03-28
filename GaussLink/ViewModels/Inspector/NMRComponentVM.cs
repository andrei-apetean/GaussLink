
using GaussLink.Data;
using GaussLink.Data.Store;
using GaussLink.Models;
using GaussLink.ViewModels.Base;
using GaussLink.Views.Windows.FileSaver;
using GaussLink.Views.Windows.Graph;
using System;
using System.Windows.Input;

namespace GaussLink.ViewModels
{
    public class NMRComponentVM : ComponentVM
    {
        public ICommand NmrSpecCommand => new RelayCommand(NmrSpec);

        private void NmrSpec()
        {
            jobFile = DataManager.SelectedJobFile;
            NMR n = Extractor.ExtractNMRdata(jobFile);
            NmrGraphWindow gw = new NmrGraphWindow(n);
            gw.Show();
        }

        public ICommand SaveCommand => new RelayCommand(Save);

        public virtual void Save()
        {
            //jobFile = DataManager.SelectedJobFile;
            DataManager.JobsToBeSaved.Add(DataManager.SelectedJobFile);
            FileSaverWindow fs = new FileSaverWindow();
            fs.Show();
        }
    }
}

using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.DataAccess;
using GaussLink.Data.Messages;
using GaussLink.Data.Store;
using GaussLink.Models;
using GaussLink.ViewModels.Base;
using System.Windows.Input;

namespace GaussLink.ViewModels
{
    public class ComponentVM : BaseViewModel
    {
        public JobFile jobFile = new JobFile();

        public ICommand GetJobContentCommand => new RelayCommand(GetJobContent);
        public virtual void GetJobContent()
        {
            jobFile = DataManager.SelectedJobFile;
            Messenger.Default.Send(new DataMessage("Content", jobFile));
        }
     
        public ICommand RemoveJobCommand => new RelayCommand(RemoveJob);

        public virtual void RemoveJob()
        {
            jobFile = DataManager.SelectedJobFile;
            Messenger.Default.Send(new RemoveJobMessage(jobFile.JobName));
        }
    }
}

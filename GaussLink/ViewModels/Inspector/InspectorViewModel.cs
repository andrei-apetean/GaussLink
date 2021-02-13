using GalaSoft.MvvmLight.Messaging;
using GaussLink.Models;

namespace GaussLink.ViewModels
{
    public class InspectorViewModel : BaseViewModel
    {
        #region base
        private ComponentVM viewModel;
        public ComponentVM ViewModel
        {
            get { return viewModel; }
            set
            {
                viewModel = value;
                OnPropertyChanged(nameof(ViewModel));
            }
        }

        public InspectorViewModel()
        {
            Messenger.Default.Register<SelectionChangedMessage>(this, OnSelectionChanged);
        }



        private void OnSelectionChanged(SelectionChangedMessage obj)
        {
            if (obj.Selection == null)
            {
                ViewModel = null;
                return;
            }
            switch (obj.JobFile.Type)
            {
                case JobType.OPT:
                    ViewModel = new OptComponentVM();
                    break;
                case JobType.FREQ:
                    ViewModel = new FreqComponentVM();
                    break;
                case JobType.TD:
                    ViewModel = new TDComponentVM();
                    break;
                case JobType.NMR:
                    ViewModel = new NMRComponentVM();
                    break;
                default:
                    ViewModel = null;
                    break;
            }
        }
        #endregion

    }
}

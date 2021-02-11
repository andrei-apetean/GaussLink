
using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data;
using GaussLink.Data.DataAccess;
using GaussLink.Data.Messages;
using GaussLink.Data.Store;
using GaussLink.ViewModels.Base;
using GaussLink.ViewModels.Themes;
using System.Windows.Input;

namespace GaussLink.ViewModels
{
    public class TDComponentVM:ComponentVM
    {
        public TDComponentVM()
        {

            Messenger.Default.Register<ThemeChangedMessage>(this, OnThemeChanged);
            OnThemeChanged(new ThemeChangedMessage(ThemesController.CurrentTheme));
        }
        #region Icons
        private void OnThemeChanged(ThemeChangedMessage obj)
        {
            switch (obj.ThemeType)
            {
                case ThemeType.Dark:
                    ContentIcon = "/UI/Images/contentIconWhite.png";
                    SaveFileIcon = "/UI/Images/saveFileIconWhite.png";
                    DeleteIcon = "/UI/Images/deleteFileIconWhite.png";
                    break;
                case ThemeType.ColourfulDark:
                    DeleteIcon = "/UI/Images/deleteFileIconWhite.png";
                    ContentIcon = "/UI/Images/contentIconWhite.png";
                    SaveFileIcon = "/UI/Images/saveFileIconWhite.png";
                    break;
                case ThemeType.Light:
                    DeleteIcon = "/UI/Images/deleteFileIconBlack.png";
                    SaveFileIcon = "/UI/Images/saveFileIconBlack.png";
                    ContentIcon = "/UI/Images/contentIconBlack.png";
                    break;
                case ThemeType.ColourfulLight:
                    DeleteIcon = "/UI/Images/deleteFileIconBlack.png";
                    SaveFileIcon = "/UI/Images/saveFileIconBlack.png";
                    ContentIcon = "/UI/Images/contentIconBlack.png";
                    break;
            }
        }

        private string contentIcon;
        public string ContentIcon
        {
            get { return contentIcon; }
            set
            {
                contentIcon = value;
                OnPropertyChanged(nameof(ContentIcon));
            }
        }

       

        private string saveFileIcon;
        public string SaveFileIcon
        {
            get { return saveFileIcon; }
            set
            {
                saveFileIcon = value;
                OnPropertyChanged(nameof(SaveFileIcon));
            }
        }

        private string deleteIcon;
        public string DeleteIcon
        {
            get { return deleteIcon; }
            set
            {
                deleteIcon = value;
                OnPropertyChanged(nameof(DeleteIcon));
            }
        }

        #endregion

        public ICommand GetExcitationEnergiesCommand => new RelayCommand(GetExcitationEnergies);

        public virtual void GetExcitationEnergies()
        {
            Messenger.Default.Send(new DataMessage("Excitation Energy",DataManager.SelectedJobFile));
        }
        public ICommand UvVisSpecCommand => new RelayCommand(UvVisSpectrum);

        public virtual void UvVisSpectrum()
        {
            Messenger.Default.Send(new DataMessage("UvVis", DataManager.SelectedJobFile));
        }
        public ICommand SaveEnergyDataCommand => new RelayCommand(SaveEnergyData);

        public virtual void SaveEnergyData()
        {
            jobFile = DataManager.SelectedJobFile;
            string content = Extractor.ExtractEnergyData(jobFile);
            FileManager.SaveText(jobFile.JobName + "_excitation_energies", content);
        }
    }
}

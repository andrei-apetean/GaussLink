using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data;
using GaussLink.Data.DataAccess;
using GaussLink.Data.Messages;
using GaussLink.Data.Store;
using GaussLink.Models;
using GaussLink.ViewModels.Base;
using GaussLink.ViewModels.Themes;
using System;
using System.Windows.Input;

namespace GaussLink.ViewModels
{
    public class FreqComponentVM:ComponentVM
    {
        public FreqComponentVM()
        {
            ModeCount = DataManager.VibrationModes.Count;
            Messenger.Default.Register<ThemeChangedMessage>(this, OnThemeChanged);
            OnThemeChanged(new ThemeChangedMessage(ThemesController.CurrentTheme));
        }
        #region Icons
        private void OnThemeChanged(ThemeChangedMessage obj)
        {
            switch(obj.ThemeType)
            {
                case ThemeType.Dark:
                    ContentIcon = "/UI/Images/contentIconWhite.png";
                    IncrementIcon = "/UI/Images/collapseArrowWhite.png";
                    DecrementIcon = "/UI/Images/expandArrowWhite.png";
                    View3DIcon = "/UI/Images/view3DIconWhite.png";
                    FreqDataIcon = "/UI/Images/frequencyDataIconWhite.png";
                    SaveFileIcon = "/UI/Images/saveFileIconWhite.png";
                    DeleteIcon = "/UI/Images/deleteFileIconWhite.png";
                    break;
                case ThemeType.ColourfulDark:
                    FreqDataIcon = "/UI/Images/frequencyDataIconWhite.png";
                    DeleteIcon = "/UI/Images/deleteFileIconWhite.png";
                    IncrementIcon = "/UI/Images/collapseArrowWhite.png";
                    DecrementIcon = "/UI/Images/expandArrowWhite.png";
                    View3DIcon = "/UI/Images/view3DIconWhite.png";
                    ContentIcon = "/UI/Images/contentIconWhite.png";
                    SaveFileIcon = "/UI/Images/saveFileIconWhite.png";
                    break;
                case ThemeType.Light:
                    FreqDataIcon = "/UI/Images/frequencyDataIconBlack.png";
                    DeleteIcon = "/UI/Images/deleteFileIconBlack.png";
                    IncrementIcon = "/UI/Images/collapseArrowBlack.png";
                    DecrementIcon = "/UI/Images/expandArrowBlack.png";
                    View3DIcon = "/UI/Images/view3DIconBlack.png";
                    SaveFileIcon = "/UI/Images/saveFileIconBlack.png";
                    ContentIcon = "/UI/Images/contentIconBlack.png";
                    break;
                case ThemeType.ColourfulLight:
                    FreqDataIcon = "/UI/Images/frequencyDataIconBlack.png";
                    View3DIcon = "/UI/Images/view3DIconBlack.png";
                    IncrementIcon = "/UI/Images/collapseArrowBlack.png";
                    DecrementIcon = "/UI/Images/expandArrowBlack.png";
                    DeleteIcon = "/UI/Images/deleteFileIconBlack.png";
                    SaveFileIcon = "/UI/Images/saveFileIconBlack.png";
                    ContentIcon = "/UI/Images/contentIconBlack.png";
                    break;
            }
        }

        private  string contentIcon;
        public  string ContentIcon
        {
            get { return contentIcon; }
            set
            { 
                contentIcon = value;
                OnPropertyChanged(nameof(ContentIcon));
            }
        }
        private string incrementIcon;
        public string IncrementIcon
        {
            get { return incrementIcon; }
            set
            {
                incrementIcon = value;
                OnPropertyChanged(nameof(IncrementIcon));
            }
        }

        private string decrementIcon;
        public string DecrementIcon
        {
            get { return decrementIcon; }
            set
            {
                decrementIcon = value;
                OnPropertyChanged(nameof(DecrementIcon));
            }
        }

        private string freqDataIcon;
        public string FreqDataIcon
        {
            get { return freqDataIcon; }
            set
            {
                freqDataIcon = value;
                OnPropertyChanged(nameof(FreqDataIcon));
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

        private string view3DIcon;
        public string View3DIcon
        {
            get { return view3DIcon; }
            set
            {
                view3DIcon = value;
                OnPropertyChanged(nameof(View3DIcon));
            }
        }
#endregion

        private VibrationMode vibrationMode;
        private int index=1;
        private  int modeCount;
        public  int  ModeCount
        {
            get { return modeCount; }
            set 
            {
                modeCount = value; 
                OnPropertyChanged(nameof(ModeCount));
            }
        }
        private string selectedMode="1";

        public string SelectedMode
        {
            get { return selectedMode; }
            set
            {
                selectedMode = value;
                OnPropertyChanged(nameof(SelectedMode));
                OnSelectedModeChanged(selectedMode);
            }
        }
        private void OnSelectedModeChanged(string selectedMode)
        {
            if (string.IsNullOrEmpty(selectedMode))
            {
                index = 1;

            }
            else
            {
                int i = int.Parse(selectedMode);
                if (1 <= i )
                {
                    index = i;

                }
                if(i>ModeCount)
                {
                    index = ModeCount;
                }
            }
        }

    
        public ICommand SaveFreqCommand => new RelayCommand(SaveFreq);

        public virtual void SaveFreq()
        {
            jobFile = DataManager.SelectedJobFile;
            string content = Extractor.ExtractFreqData(jobFile);
            FileManager.SaveText(jobFile.JobName + "_freqData",content);
        }

        public ICommand GetFreqDataCommand => new RelayCommand(GetFreqData);

        public virtual void GetFreqData()
        {
            jobFile = DataManager.SelectedJobFile;
            Messenger.Default.Send(new DataMessage("FreqData", jobFile));
        }

        public ICommand GetVibrationModeDataCommand => new RelayCommand(GetVibrationModeData);

        public virtual void GetVibrationModeData()
        {
            vibrationMode = DataManager.VibrationModes[index-1];
            string name = DataManager.SelectedJobFile.JobName;
            Messenger.Default.Send(new DataMessage("Vibration Mode", name+"_mode_"+index, vibrationMode));
        }
        public ICommand Vibration3DCommand => new RelayCommand(Vibration3D);

        public virtual void Vibration3D()
        {
            jobFile = DataManager.SelectedJobFile;
            Messenger.Default.Send(new DataMessage("3D Structure",jobFile,false,false));
        }

        public ICommand IncrementCommand => new RelayCommand(Increment);

        private void Increment()
        {
            int.TryParse(SelectedMode, out int s);
            s++;
            SelectedMode = s.ToString();
        }
        public ICommand DecrementCommand => new RelayCommand(Decrement);

        private void Decrement()
        {
            int.TryParse(SelectedMode, out int s);
            s--;
            SelectedMode = s.ToString();
        }
    }
}

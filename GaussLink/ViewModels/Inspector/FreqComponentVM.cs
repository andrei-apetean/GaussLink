using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data;
using GaussLink.Data.DataAccess;
using GaussLink.Data.Messages;
using GaussLink.Data.Store;
using GaussLink.Models;
using GaussLink.ViewModels.Base;
using System.Windows.Input;

namespace GaussLink.ViewModels
{
    public class FreqComponentVM:ComponentVM
    {
        public FreqComponentVM()
        {
            ModeCount = DataManager.VibrationModes.Count;
        }
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

    }
}

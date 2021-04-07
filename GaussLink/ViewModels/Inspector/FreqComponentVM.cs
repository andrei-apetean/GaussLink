using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data;
using GaussLink.Data.DataAccess;
using GaussLink.Data.Messages;
using GaussLink.Data.Store;
using GaussLink.Models;
using GaussLink.ViewModels.Base;
using GaussLink.ViewModels.Themes;
using GaussLink.ViewModels.Windows.FileSaver;
using GaussLink.Views.Windows;
using GaussLink.Views.Windows.FileSaver;
using System.Windows.Input;

namespace GaussLink.ViewModels
{
    public class FreqComponentVM : ComponentVM
    {
        public FreqComponentVM()
        {
            ModeCount = DataManager.VibrationModes.Count;
           
        }
    
        private VibrationMode vibrationMode;
        private int index = 1;
        private int modeCount;
        public int ModeCount
        {
            get { return modeCount; }
            set
            {
                modeCount = value;
                OnPropertyChanged(nameof(ModeCount));
            }
        }
        private string selectedMode = "1";

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
                if (1 <= i)
                {
                    index = i;

                }
                if (i > ModeCount)
                {
                    index = ModeCount;
                    SelectedMode = ModeCount.ToString();
                }
            }
        }


        public ICommand SaveCommand => new RelayCommand(Save);

        public virtual void Save()
        {
            //jobFile = DataManager.SelectedJobFile;
            DataManager.JobsToBeSaved.Add(DataManager.SelectedJobFile);
            FileSaverWindow fs = new FileSaverWindow();
            fs.Show();
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
            vibrationMode = DataManager.VibrationModes[index - 1];
            string name = DataManager.SelectedJobFile.JobName;
            Messenger.Default.Send(new DataMessage("Vibration Mode", name + "_mode_" + index, vibrationMode));
        }
        public ICommand Viewer3DCommand => new RelayCommand(NewViewer3D);

        public virtual void NewViewer3D()
        {
            jobFile = DataManager.SelectedJobFile;
            Molecule3D m = Extractor.ExtractMolecule3D(jobFile, false, true);
            //Messenger.Default.Send(new DataMessage("3D Structure",jobFile,false,false));
            Viewer3D viewer3D = new Viewer3D(m);
            viewer3D.Show();
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

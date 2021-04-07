using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data;
using GaussLink.Data.DataAccess;
using GaussLink.Data.Messages;
using GaussLink.Data.Store;
using GaussLink.Models;
using GaussLink.ViewModels.Base;
using GaussLink.ViewModels.Themes;
using GaussLink.Views.Windows;
using GaussLink.Views.Windows.FileSaver;
using System.Windows.Input;

namespace GaussLink.ViewModels
{
    public class OptComponentVM : ComponentVM
    {
        public OptComponentVM()
        {

        }

        private bool inputOrientation = true;
        public bool InputOrientation
        {
            get { return inputOrientation; }
            set
            {
                inputOrientation = value;
                OnPropertyChanged(nameof(InputOrientation));
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


        public ICommand Viewer3DCommand => new RelayCommand(NewViewer3D);

        public virtual void NewViewer3D()
        {
            jobFile = DataManager.SelectedJobFile;
            Molecule3D m = Extractor.ExtractMolecule3D(jobFile, true, true);
            //Messenger.Default.Send(new DataMessage("3D Structure", jobFile, true, !inputOrientation));
            Viewer3D viewer3D = new Viewer3D(m);
            viewer3D.Show();
        }

        public ICommand GetOrientationCommand => new RelayCommand(GetOrientation);

        public virtual void GetOrientation()
        {
            jobFile = DataManager.SelectedJobFile;
            Messenger.Default.Send(new DataMessage("Orientation", jobFile, inputOrientation));
        }
        public ICommand SaveOrientationCommand => new RelayCommand(SaveOrientation);

        public virtual void SaveOrientation()
        {
            jobFile = DataManager.SelectedJobFile;
            MoleculeOrientation o = Extractor.ExtractOrientation(jobFile, InputOrientation);
            string s = InputOrientation ? "_Input_Orientation" : "_Standard_Orientation";
            FileManager.SaveOrientation(jobFile.JobName + s, o);
        }
       
    }


}

using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data;
using GaussLink.Data.DataAccess;
using GaussLink.Data.Messages;
using GaussLink.Data.Store;
using GaussLink.Models;
using GaussLink.ViewModels.Base;
using GaussLink.ViewModels.Themes;
using System.Windows.Input;

namespace GaussLink.ViewModels
{
    public class OptComponentVM:ComponentVM
    {
        public OptComponentVM()
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
                    View3DIcon = "/UI/Images/view3DIconWhite.png";
                    OrientationIcon = "/UI/Images/orientationIconWhite.png";
                    SaveFileIcon = "/UI/Images/saveFileIconWhite.png";
                    DeleteIcon = "/UI/Images/deleteFileIconWhite.png";
                    break;
                case ThemeType.ColourfulDark:
                    OrientationIcon = "/UI/Images/orientationIconWhite.png";
                    DeleteIcon = "/UI/Images/deleteFileIconWhite.png";
                    View3DIcon = "/UI/Images/view3DIconWhite.png";
                    ContentIcon = "/UI/Images/contentIconWhite.png";
                    SaveFileIcon = "/UI/Images/saveFileIconWhite.png";
                    break;
                case ThemeType.Light:
                    OrientationIcon = "/UI/Images/orientationIconBlack.png";
                    DeleteIcon = "/UI/Images/deleteFileIconBlack.png";
                    View3DIcon = "/UI/Images/view3DIconBlack.png";
                    SaveFileIcon = "/UI/Images/saveFileIconBlack.png";
                    ContentIcon = "/UI/Images/contentIconBlack.png";
                    break;
                case ThemeType.ColourfulLight:
                    OrientationIcon = "/UI/Images/orientattionIconBlack.png";
                    View3DIcon = "/UI/Images/view3DIconBlack.png";
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

        private string orientationIcon;
        public string OrientationIcon
        {
            get { return orientationIcon; }
            set
            {
                orientationIcon = value;
                OnPropertyChanged(nameof(OrientationIcon));
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
       
        public ICommand Get3DStructureCommand => new RelayCommand(Get3DStructure);

        public virtual void Get3DStructure()
        {
            jobFile = DataManager.SelectedJobFile;
            Messenger.Default.Send(new DataMessage("3D Structure", jobFile, true, !inputOrientation));
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
        //public ICommand GetJobContentCommand => new RelayCommand(GetJobContent);

        //public virtual void GetJobContent()
        //{
        //    jobFile = DataManager.SelectedJobFile;
        //    Messenger.Default.Send(new DataMessage("Content", jobFile));
        //}

        //public ICommand SaveContentCommand => new RelayCommand(SaveContent);

        //public virtual void SaveContent()
        //{
        //    jobFile = DataManager.SelectedJobFile;
        //    FileManager.SaveJobFileContent(jobFile);
        //}



        //public ICommand RemoveJobCommand => new RelayCommand(RemoveJob);

        //public virtual void RemoveJob()
        //{
        //    jobFile = DataManager.SelectedJobFile;
        //    Messenger.Default.Send(new RemoveJobMessage(jobFile.JobName));
        //}
    }

    
}

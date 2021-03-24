using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data;
using GaussLink.Data.DirStruct;
using GaussLink.Data.Messages;
using GaussLink.Data.Store;
using GaussLink.Models;
using GaussLink.Properties;
using GaussLink.ViewModels.Windows.FileBrowser;
using GaussLink.Views.Windows.FileSaver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GaussLink.ViewModels.Windows.FileSaver
{
    public class FileSaverViewModel : BaseViewModel
    {
        public FileSaverViewModel()
        {
            InitItems();
            var children = DirectoryStructure.GetLogicalDrives();
            currentDirectories = new ObservableCollection<DirectoryItemViewModel>();
            this.CloseWindowCommand = new RelayCommand<Window>(this.CloseWindow);
            this.SaveFileCommand = new RelayCommand<Window>(this.SaveFile);
            // Create the view models from the data
            this.Directories = new ObservableCollection<DirectoryItemViewModel>(children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));
            Messenger.Default.Register<FileBrowserTvSelectMessage>(this, OnTvSelectionChanged);
            Messenger.Default.Register<FileBrowserLvSelectMessage>(this, OnLvSelectionChanged);
            if (!string.IsNullOrEmpty(Settings.Default.LastOpenedPath))
            {
                SelectedDirectory = new DirectoryItemViewModel(Settings.Default.LastOpenedPath, DirectoryItemType.Folder);
                GetDirectoryContents(SelectedDirectory, false);
            }
        }
        public RelayCommand<Window> CloseWindowCommand { get; private set; }

        void InitItems()
        {
            Items = new ObservableCollection<SaveDialog>();
            foreach (var item in DataManager.JobsToBeSaved)
            {
                switch (item.Type)
                {
                    case JobType.OPT:
                        Items.Add(new OptSaveDialog(item));
                        break;

                    case JobType.FREQ:
                        Items.Add(new FreqSaveDialog(item));
                        break;

                    case JobType.TD:
                        Items.Add(new TdSaveDialog(item));
                        break;

                }
            }
            
        }

        private void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        public RelayCommand<Window> SaveFileCommand { get; private set; }

        private string folderName="New Folder";

        public string FolderName
        {
            get { return folderName; }
            set { folderName = value; OnPropertyChanged(nameof(FolderName)); }
        }



        private void SaveFile(Window window)
        {
            if (SelectedDirectory == null)
            {
                
                if (window != null)
                {
                    window.Close();
                }
                return;
            }
            string path = SelectedDirectory.FullPath;
            OptSaveDialog o = new OptSaveDialog();
            FreqSaveDialog f = new FreqSaveDialog();
            TdSaveDialog t = new TdSaveDialog();
            foreach (var item in Items)
            {
                if(item is OptSaveDialog)
                {
                     }

                switch (item.JobFile.Type)
                {
                    case JobType.OPT:
                        o = (OptSaveDialog)item;
                        SaveOptimizationJob(o,path);
                        break;
                    case JobType.FREQ:
                        f = (FreqSaveDialog)item;
                        SaveFreqJob(f,path);
                        break;
                    case JobType.TD:
                        t = (TdSaveDialog)item;
                        SaveTDJob(t, path);
                        break;
                    case JobType.NMR:
                        //SaveNMRJob(item);
                        break;
                }

            }

            if (window != null)
            {
                window.Close();
            }
        }

        //private void SaveNMRJob(NSaveDialog item)
        //{
        //    throw new NotImplementedException();
        //}

        private void SaveTDJob(TdSaveDialog item, string path)
        {
            if (item.AsJob)
            {
                string p = Path.Combine(path, item.Name + ".out");
                string output = string.Join(Environment.NewLine, item.JobFile.Content.ToArray());
                File.WriteAllText(p, output);
            }
            if (item.Content)
            {
                string p = Path.Combine(path, item.Name + ".txt");
                string output = string.Join(Environment.NewLine, item.JobFile.Content.ToArray());
                File.WriteAllText(p, output);
            }
            if(item.ExcitationEnergies)
            {
                string p = Path.Combine(path, item.Name + "_excitation_energies.txt");
                ExcitationEnergy e = Extractor.ExtractExcitationEnergies(item.JobFile);
                File.WriteAllText(p, e.ToString());
            }
        }

        private void SaveFreqJob(FreqSaveDialog item, string path)
        {
            if (item.AsJob)
            {
                string p = Path.Combine(path, item.Name + ".out");
                string output = string.Join(Environment.NewLine, item.JobFile.Content.ToArray());
                File.WriteAllText(p, output);
            }
            if (item.Content)
            {
                string p = Path.Combine(path, item.Name + ".txt");
                string output = string.Join(Environment.NewLine, item.JobFile.Content.ToArray());
                File.WriteAllText(p, output);
            }
            if(item.VibrationModes)
            {
                string p = Path.Combine(path, item.Name + "_vibration_modes.txt");
                List<VibrationMode> m = Extractor.ExtractVibrationModes(item.JobFile);
                List<string> s = m.ConvertAll(x => x.ToString());
                string output = string.Join(Environment.NewLine, s.ToArray());
                File.WriteAllText(p, output);

            }
        }


        private void SaveOptimizationJob(OptSaveDialog item, string path)
        {
            if(item.AsJob)
            {
                string p = Path.Combine(path, item.Name+".out");
                string output = string.Join(Environment.NewLine, item.JobFile.Content.ToArray());
                File.WriteAllText(p, output);
            }
            if (item.Content)
            {
                string p = Path.Combine(path, item.Name + ".txt");
                string output = string.Join(Environment.NewLine, item.JobFile.Content.ToArray());
                File.WriteAllText(p, output);
            }
            if (item.InputOrientation)
            {
                string p = Path.Combine(path, item.Name + "_input_orientation.txt");

                MoleculeOrientation orientation = Extractor.ExtractOrientation(item.JobFile, true);
                File.WriteAllText(p, orientation.ToString());
            }
            if (item.StandardOrientation)
            {
                string p = System.IO.Path.Combine(path, item.Name + "_standard_orientation.txt");

                MoleculeOrientation orientation = Extractor.ExtractOrientation(item.JobFile, false);
                File.WriteAllText(p, orientation.ToString());
            }
        }

        private void OnLvSelectionChanged(FileBrowserLvSelectMessage obj)
        {
            //Items = obj.DirectoryItemViewModels;

        }


        public ICommand NewFolderCommand => new RelayCommand(NewFolder);

        private void NewFolder()
        {
            if(SelectedDirectory !=null)
            {
                Directory.CreateDirectory(Path.Combine(SelectedDirectory.FullPath, FolderName));
                GetDirectoryContents(SelectedDirectory, false);
            }
        }

        private void OnTvSelectionChanged(FileBrowserTvSelectMessage obj)
        {
            SelectedItem = obj.DirectoryItemViewModel;
            GetDirectoryContents(obj.DirectoryItemViewModel,false);

        }

        //private string path;
        //public string Path
        //{
        //    get { return path; }
        //    set
        //    {
        //        path = value;
        //        OnPropertyChanged(nameof(Path));
        //    }
        //}


        private DirectoryItemViewModel selectedItem;
        public DirectoryItemViewModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                SelectedDirectory = selectedItem;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private DirectoryItemViewModel selectedDirectory;
        public DirectoryItemViewModel SelectedDirectory
        {
            get { return selectedDirectory; }
            set
            {
                selectedDirectory = value;
                SelectedDirectoryChanged();
                OnPropertyChanged(nameof(SelectedDirectory));
            }
        }

        private void SelectedDirectoryChanged()
        {
            Messenger.Default.Send(new DirectorySelectMessage(SelectedDirectory));
        }

        private ObservableCollection<DirectoryItemViewModel> directories;

        public ObservableCollection<DirectoryItemViewModel> Directories
        {
            get { return directories; }
            set
            {
                directories = value;
                OnPropertyChanged(nameof(Directories));
            }
        }
        private ObservableCollection<DirectoryItemViewModel> currentDirectories;

        public ObservableCollection<DirectoryItemViewModel> CurrentDirectories
        {
            get { return currentDirectories; }
            set
            {
                currentDirectories = value;
                OnPropertyChanged(nameof(CurrentDirectories));
            }
        }
        private ObservableCollection<SaveDialog> items = new ObservableCollection<SaveDialog>();

        public ObservableCollection<SaveDialog> Items
        {
            get { return items; }
            set
            {
                items = value;
                //Path = "";
                //foreach (var e in Items)
                //{
                //    if (e.Type == DirectoryItemType.File)
                //    {
                //        //FilePaths.Add(e.FullPath);
                //        Path += "\"" + e.Name + "\" ";
                //    }
                //}
                OnPropertyChanged(nameof(Items));

            }
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);

        private void GoBack()
        {
            if (selectedDirectory.Type == DirectoryItemType.Drive) { return; }
            string n = SelectedDirectory.Name;
            string fp = SelectedDirectory.FullPath;
            fp = fp.Remove(fp.Length - (n.Length + 1));
            if (fp.Length <= 3)
            {
                var drives = DirectoryStructure.GetLogicalDrives();
                string dirPath = drives.FirstOrDefault(p => p.FullPath.Contains(fp)).FullPath;
                SelectedDirectory = new DirectoryItemViewModel(dirPath, DirectoryItemType.Drive);
                GetDirectoryContents(SelectedDirectory,false);
                return;
            }
            SelectedDirectory = new DirectoryItemViewModel(fp, DirectoryItemType.Folder);
            GetDirectoryContents(SelectedDirectory,false);
        }

        private void GetDirectoryContents(DirectoryItemViewModel d, bool foldersOnly)
        {
            if (d.Type == DirectoryItemType.File)
                return;

            // Find all children
            var children = DirectoryStructure.GetDirectoryContents(d.FullPath, foldersOnly);
            CurrentDirectories = new ObservableCollection<DirectoryItemViewModel>(children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));

        }
    }
}


using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.DirStruct;
using GaussLink.Data.Messages;
using GaussLink.Properties;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;


namespace GaussLink.ViewModels.Windows.FileBrowser
{
    public class FileBrowserViewModel : BaseViewModel
    {

        public FileBrowserViewModel()
        {
            var children = DirectoryStructure.GetLogicalDrives();
            currentItems = new ObservableCollection<DirectoryItemViewModel>();
            this.CloseWindowCommand = new RelayCommand<Window>(this.CloseWindow);
            this.OpenFileCommand= new RelayCommand<Window>(this.OpenFile);
            // Create the view models from the data
            this.Items = new ObservableCollection<DirectoryItemViewModel>(children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));
            Messenger.Default.Register<FileBrowserTvSelectMessage>(this, OnTvSelectionChanged);
            Messenger.Default.Register<FileBrowserLvSelectMessage>(this, OnLvSelectionChanged);
            if(!string.IsNullOrEmpty(Settings.Default.LastOpenedPath))
            {
                SelectedDirectory = new DirectoryItemViewModel(Settings.Default.LastOpenedPath, DirectoryItemType.Folder);
                GetDirectoryContents(SelectedDirectory, false);
            }
        }
        public RelayCommand<Window> CloseWindowCommand { get; private set; }

        

        private void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        public RelayCommand<Window> OpenFileCommand { get; private set; }

        private void OpenFile(Window window)
        {
            if (SelectedDirectories.Count > 1)
            {
                    FilePaths = new List<string>();
                    foreach (var item in SelectedDirectories)
                    {
                        if (item.Type == DirectoryItemType.File) FilePaths.Add(item.FullPath);
                    }
                    Messenger.Default.Send(new FileExOpenFileMessage(FilePaths));
                    Settings.Default.LastOpenedPath = SelectedDirectory.FullPath;
                    Settings.Default.Save();
                    if (window != null)
                    {
                        window.Close();
                    }
                    return;
                
            }
            else if (SelectedDirectories[0].Type != DirectoryItemType.File)
            {
                SelectedDirectory = SelectedDirectories[0];
                GetDirectoryContents(SelectedDirectory, false);
                return;
            }

            FilePaths = new List<string>();
            foreach (var item in SelectedDirectories)
            {
                if (item.Type == DirectoryItemType.File) FilePaths.Add(item.FullPath);
            }
            Messenger.Default.Send(new FileExOpenFileMessage(FilePaths));
            Settings.Default.LastOpenedPath = SelectedDirectory.FullPath;
            Settings.Default.Save();
            if (window != null)
            {
                window.Close();
            }

        }

        private void OnLvSelectionChanged(FileBrowserLvSelectMessage obj)
        {
            SelectedDirectories = obj.DirectoryItemViewModels;

        }


        List<string> FilePaths = new List<string>();

        private void OnTvSelectionChanged(FileBrowserTvSelectMessage obj)
        {
            if(obj.DirectoryItemViewModel.Type != DirectoryItemType.File)
            { 
                SelectedItem = obj.DirectoryItemViewModel;
                GetDirectoryContents(obj.DirectoryItemViewModel, false);
            }

        }

        private string file;
        public string File
        {
            get { return file; }
            set
            {
                file = value;
                OnPropertyChanged(nameof(File));
            }
        }


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

        private ObservableCollection<DirectoryItemViewModel> items;

        public ObservableCollection<DirectoryItemViewModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        private ObservableCollection<DirectoryItemViewModel> currentItems;

        public ObservableCollection<DirectoryItemViewModel> CurrentItems
        {
            get { return currentItems; }
            set
            {
                currentItems = value;
                OnPropertyChanged(nameof(CurrentItems));
            }
        }
        private ObservableCollection<DirectoryItemViewModel> selectedDirectories = new ObservableCollection<DirectoryItemViewModel>();

        public ObservableCollection<DirectoryItemViewModel> SelectedDirectories
        {
            get { return selectedDirectories; }
            set
            {
                selectedDirectories = value;
                File = "";
                foreach (var e in SelectedDirectories)
                {
                   if(e.Type == DirectoryItemType.File)
                    {
                        //FilePaths.Add(e.FullPath);
                        File += "\""+e.Name + "\" ";
                    }
                }
                OnPropertyChanged(nameof(SelectedDirectories));

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
            CurrentItems = new ObservableCollection<DirectoryItemViewModel>(children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));

        }
    }
}

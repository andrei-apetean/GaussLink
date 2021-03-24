using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.DirStruct;
using GaussLink.Data.Messages;
using GaussLink.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GaussLink.ViewModels.Windows.FileBrowser
{
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        private string imageName;
        public string ImageName
        {
            get { return imageName; }
            set
            {
                imageName = value;
                OnPropertyChanged(nameof(ImageName));
            }
        }
        /// <summary>
        /// The full path to the item
        /// </summary>
        public string FullPath { get; set; }

        private string name;
        public string Name
        {
            get
            {
                name = this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath);
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }

        }
        private ObservableCollection<DirectoryItemViewModel> children = new ObservableCollection<DirectoryItemViewModel>();
        public ObservableCollection<DirectoryItemViewModel> Children
        {
            get { return children; }
            set
            {
                children = value;
                OnPropertyChanged(nameof(Children));
            }
        }


        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        /// <summary>
        /// Indicates if the current item is expanded or not
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                // If the UI tells us to expand...
                if (value == true)
                    // Find all children
                    Expand();
                // If the UI tells us to close
                else
                    this.ClearChildren();
            }
        }

        private bool isTreeViewSelected;
        public bool IsTreeViewSelected
        {
            get { return isTreeViewSelected; }
            set
            {
                isTreeViewSelected = value;
                if (isTreeViewSelected)
                {
                    IsExpanded = true;
                    Messenger.Default.Send(new FileBrowserTvSelectMessage(this));
                }
            }
        }

        #endregion

        #region Public Commands
        public ICommand AccessDirectoryComand => new RelayCommand(AccessDirectory);
        private void AccessDirectory()
        {
            Messenger.Default.Send(new FileBrowserTvSelectMessage(this));
        }

        /// <summary>
        /// The command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fullPath">The full path of this item</param>
        /// <param name="type">The type of item</param>
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            // Create commands
            this.ExpandCommand = new RelayCommand(Expand);

            // Set path and type
            this.FullPath = fullPath;
            this.Type = type;
            this.ImageName = Type == DirectoryItemType.Drive ? "drive" : (Type == DirectoryItemType.File ? "file" : (IsExpanded ? "folder-open" : "folder-closed"));


            // Setup the children as needed
            this.ClearChildren();
            if (this.Type == DirectoryItemType.File)
                return;



            Messenger.Default.Register<DirectorySelectMessage>(this, SelectionChanged);
        }

        private void SelectionChanged(DirectorySelectMessage obj)
        {
            if (this != obj.DirectoryItemViewModel) { IsTreeViewSelected = false; }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Removes all children from the list, adding a dummy item to show the expand icon if required
        /// </summary>
        private void ClearChildren()
        {
            // Clear items
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            // Show the expand arrow if we are not a file
            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }

        #endregion

        /// <summary>
        ///  Expands this directory and finds all children
        /// </summary>
        private void Expand()
        {
            // We cannot expand a file
            if (this.Type == DirectoryItemType.File)
                return;

            // Find all children
            var children = DirectoryStructure.GetDirectoryContents(this.FullPath, true);
            this.Children = new ObservableCollection<DirectoryItemViewModel>(
                                children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
            this.ImageName = Type == DirectoryItemType.Drive ? "drive" : (Type == DirectoryItemType.File ? "file" : (IsExpanded ? "folder-open" : "folder-closed"));

        }
    }
}

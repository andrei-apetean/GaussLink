using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data;
using GaussLink.Data.DataAccess;
using GaussLink.Data.Messages;
using GaussLink.Data.Store;
using GaussLink.Models;
using GaussLink.Views.Windows.FileSaver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace GaussLink.ViewModels
{
    public class ItemExplorerViewModel : BaseViewModel
    {
        #region Constructor
        public ItemExplorerViewModel()
        {
            items = new ObservableCollection<ExplorerItem>();
            items.CollectionChanged += Items_CollectionChanged;
            Messenger.Default.Register<FileExOpenFileMessage>(this, OpenFiles);
            Messenger.Default.Register<SaveMessage>(this, SaveFiles);
            Messenger.Default.Register<RemoveJobMessage>(this, OnItemRemove);
        }

        private void SaveFiles(SaveMessage obj)
        {
            switch (obj.Message)
            {
                case "Selected":
                    DataManager.JobsToBeSaved.Clear();
                    if (DataManager.SelectedJobFile != null && DataManager.SelectedJobFile.Type != JobType.UNKNOWN)
                    {
                        DataManager.JobsToBeSaved.Add(DataManager.SelectedJobFile);
                        FileSaverWindow fs = new FileSaverWindow();
                        fs.Show();
                    }
                    break;
                case "All":
                    DataManager.JobsToBeSaved.Clear();
                    if (jobFiles.Count > 0)
                    {
                        IEnumerable<JobFile> j = jobFiles.Where(x => x.Type != JobType.UNKNOWN);
                        DataManager.JobsToBeSaved.AddRange(j);
                        FileSaverWindow fsw = new FileSaverWindow();
                        fsw.Show();
                    }
                    break;
            }

        }

        private void OpenFiles(FileExOpenFileMessage obj)
        {
            List<JobFile> newItems = FileManager.OpenFile(obj.FilePaths);
            if (newItems == null)
                return;
            foreach (JobFile f in newItems)
            {
                foreach (JobFile j in jobFiles)
                {
                    if (j.JobName == f.JobName)
                    {
                        f.JobName += "_copy";
                    }
                }
                Items.Add(new ExplorerItem(f.JobName));
                jobFiles.Add(f);
                Messenger.Default.Send(new FileMessage("TabDisplay"));
            }
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ExplorerItem item;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    item = (ExplorerItem)e.NewItems[0];
                    item.DeleteRequest += OnDeleteRequested;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    item = (ExplorerItem)e.OldItems[0];
                    if (Items.Count == 0) { Messenger.Default.Send(new FileMessage("OpenFile")); }
                    item.DeleteRequest -= OnDeleteRequested;
                    break;
            }
        }





        private void OnDeleteRequested(object sender, EventArgs e)
        {
            ExplorerItem ei = (ExplorerItem)sender;

            Items.Remove(ei);

            foreach (var item in jobFiles)
            {
                if (item.JobName == ei.Name)
                {
                    jobFiles.Remove(item); 
                    break;
                }
            }
        }
        #endregion

        #region Binded Variables
        private ObservableCollection<ExplorerItem> items = new ObservableCollection<ExplorerItem>();
        public ObservableCollection<ExplorerItem> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        private ExplorerItem selectedItem;
        public ExplorerItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
                OnSelectionChanged(selectedItem);
            }
        }
        #endregion

        List<JobFile> jobFiles = new List<JobFile>();

        private void OnItemRemove(RemoveJobMessage obj)
        {
            foreach (JobFile jobFile in jobFiles)
            {
                if (jobFile.JobName == obj.Message)
                {
                    jobFiles.Remove(jobFile);
                    break;
                }
            }
            foreach (var s in Items)
            {
                if (s.Name == obj.Message)
                {
                    Items.Remove(s);
                    if (Items.Count == 0)
                    {
                        SelectedItem = null;
                        Messenger.Default.Send(new FileMessage("OpenFile"));
                        break;
                    }
                    SelectedItem = null;
                    //SelectedItem = Items[0];
                    break;
                }
            }
        }

        public virtual void OnSelectionChanged(ExplorerItem SelectedItem)
        {
            if (SelectedItem == null)
            {
                Messenger.Default.Send(new SelectionChangedMessage(null));
                return;
            }
            Messenger.Default.Send(new DataMessage(selectedItem.Name));
            foreach (JobFile j in jobFiles)
            {
                if (j.JobName == selectedItem.Name)
                {
                    DataManager.SelectedJobFile = j;
                    if (j.Type == JobType.FREQ)
                    {
                        DataManager.VibrationModes = Extractor.ExtractVibrationModes(j);
                    }
                    Messenger.Default.Send(new SelectionChangedMessage(selectedItem.Name, j));
                }
            }

        }
    }
}


using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data;
using GaussLink.Data.DataAccess;
using GaussLink.Data.Messages;
using GaussLink.Data.Store;
using GaussLink.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;


namespace GaussLink.ViewModels
{
    public class ItemExplorerViewModel:BaseViewModel
    {
        #region Constructor
        public ItemExplorerViewModel()
        {
            items = new ObservableCollection<ExplorerItem>();
            items.CollectionChanged += Items_CollectionChanged;
            Messenger.Default.Register<OpenFileMessage>(this, OnOpeningFile);
            Messenger.Default.Register<RemoveJobMessage>(this, OnItemRemove);
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
            Items.Remove((ExplorerItem)sender);
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

        private void OnOpeningFile(OpenFileMessage obj)
        {
            OpenDataFile();
        }

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

        public void OpenDataFile()
        {
            List<JobFile> newItems = FileManager.OpenFile();
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


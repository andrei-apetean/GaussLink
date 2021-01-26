using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data;
using GaussLink.Data.Events;
using GaussLink.Data.Messages;
using GaussLink.Models;
using GaussLink.ViewModels.MainDisplay.Tabs;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace GaussLink.ViewModels.MainDisplay
{
    public class TabDisplayViewModel:DisplayVM
    {

        public TabDisplayViewModel()
        {
            tabs = new ObservableCollection<TabHeader>();
            tabs.CollectionChanged += Tabs_CollectionChanged;
            Tabs = tabs;
            Messenger.Default.Register<DataMessage>(this, GetJobData);
            Messenger.Default.Register<SizeChangedMessage>(this, SizeHasChanged);
       
        }

        #region TabHeader Width Handling
        private double totalWidth;
        private double headerWidth=150;


        public void SizeHasChanged(SizeChangedMessage obj)
        {
            totalWidth = obj.Width;
            headerWidth = totalWidth / Tabs.Count;
            if (headerWidth > 150) headerWidth = 150;
            OnSizeChanged();
        }

        public delegate void SizeChangedEventHandler(object sender, SizeChangedArgs e);
        public event SizeChangedEventHandler SizeChanged;

        protected virtual void OnSizeChanged()
        {
            SizeChanged?.Invoke(this, new SizeChangedArgs(0,headerWidth));
        }

        private void TabCountChanged()
        {
            headerWidth = totalWidth / Tabs.Count;
            if (headerWidth > 150) headerWidth = 150;
            OnSizeChanged();

        }
        #endregion

        #region TabHeader Events
        private void Tabs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TabHeader tab = null;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    tab = (TabHeader)e.NewItems[0];
                    tab.ClosedRequest += OnTabClosedRequested;
                    SelectedHeader = tab;
                    SwitchContent(SelectedHeader);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    tab = (TabHeader)e.OldItems[0];
                    tab.ClosedRequest -= OnTabClosedRequested;
                    if (SelectedHeader == tab) { SelectedHeader = Tabs.FirstOrDefault(); }
                    break;
            }
            TabCountChanged();
        }

        private void OnTabClosedRequested(object sender, EventArgs e)
        {
            Tabs.Remove((TabHeader)sender);
        }

       
        #endregion


        #region Bindables
        private TabContent viewModel = null;
        public TabContent ViewModel
        {
            get
            {
                return viewModel;
            }
            set
            {
                viewModel = value;
                OnPropertyChanged(nameof(ViewModel));
            }
        }

        private TabHeader selectedHeader = null;
        public TabHeader SelectedHeader
        {
            get
            {
                return selectedHeader;
            }
            set
            {
                selectedHeader = value;
                OnPropertyChanged(nameof(SelectedHeader));
                SwitchContent(SelectedHeader);
            }
        }

        private void SwitchContent(TabHeader th)
        {
            if (th==null)
            {
                ViewModel = null;
                return;
            }
            ViewModel = th.TabContent;
        }

        private ObservableCollection<TabHeader> tabs = null;
        public ObservableCollection<TabHeader> Tabs
        {
            get { return tabs; }
            set
            {
                tabs = value;
              OnPropertyChanged(nameof(Tabs));
            }
        }

        #endregion



        private void GetJobData(DataMessage obj)
        {
            switch (obj.Message)
            {
                case "Content":NewFileContentTab(obj.JobFile.JobName, obj.JobFile);
                    break;
                case "Orientation":NewOrientationTab(obj.IsInput, obj.JobFile);
                    break;
                case "3D Structure":New3DTab(obj.JobFile, obj.IsStatic, obj.IsStandard);
                    break;
                case "FreqData":NewFreqDataTab(obj.JobFile.JobName, obj.JobFile);
                    break;
                case "Vibration Mode": NewVibrationModeTab(obj.Name, obj.VibrationMode);
                    break;
                case "Excitation Energy": NewExcitationEnergyTab(obj.JobFile.JobName, obj.JobFile);
                    break;
            }
        }


        private void NewExcitationEnergyTab(string name, JobFile file)
        {
            ExcitationEnergy e = Extractor.ExtractExcitationEnergies(file);
        }

        private void New3DTab(JobFile file, bool isStatic, bool isStandard)
        {
            Molecule3D m = Extractor.ExtractMolecule3D(file, isStatic, isStandard);
            var tabHeader = new TabHeader(file.JobName, headerWidth, new Viewer3DTab(m));
            SizeChanged += tabHeader.OnSizeChanged;
            Tabs.Add(tabHeader);
        }

        private void NewVibrationModeTab(string name, VibrationMode vibrationMode)
        {
            var tabHeader = new TabHeader( name, headerWidth, (new VibrationModeTab(vibrationMode)));
            SizeChanged += tabHeader.OnSizeChanged;
            Tabs.Add(tabHeader);
        }

        private void NewFileContentTab(string Item, JobFile jobFile)
        {
            string ItemContent = Extractor.ExtractJobFileContent(jobFile);
            var tabHeader = new TabHeader(jobFile.JobName, headerWidth, new FileContentTab(ItemContent));
            SizeChanged += tabHeader.OnSizeChanged;
            Tabs.Add(tabHeader);
        }

        private void NewOrientationTab(bool isInput, JobFile file)
        {
            MoleculeOrientation orientation = Extractor.ExtractOrientation(file, isInput);
            string name = isInput ? "Input_Orientation" : "Standard_Orientation";
            string param = isInput ? "Input Orientation" : "Standard Orientation";

            var tabHeader = new TabHeader(file.JobName, headerWidth, new OrientationTab(param, orientation.Atoms));
            SizeChanged += tabHeader.OnSizeChanged;
            Tabs.Add(tabHeader);

        }


        private void NewFreqDataTab(string Item, JobFile file)
        {
            string ItemContent = Extractor.ExtractFreqData(file);
            var tabHeader = new TabHeader(file.JobName, headerWidth, new FileContentTab(ItemContent));
            SizeChanged += tabHeader.OnSizeChanged;
            Tabs.Add(tabHeader);
        }


    }


}

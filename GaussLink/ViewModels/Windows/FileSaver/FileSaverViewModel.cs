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
using System.Globalization;
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
                    case JobType.NMR:
                        Items.Add(new NmrSaveDialog(item));
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
            NmrSaveDialog n = new NmrSaveDialog();
            foreach (var item in Items)
            {
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
                        SaveNMRJob(n, path);
                        break;
                }

            }

            if (window != null)
            {
                window.Close();
            }
        }

        private void SaveNMRJob(SaveDialog item, string path)
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
        }


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
                File.WriteAllText(p, ArrangeEnergy(e));
            }
        }

        private string ArrangeEnergy(ExcitationEnergy e)
        {
            StringBuilder sb = new StringBuilder();
            int idSize = 15, mSize = 16, evSize = 14, cmSize = 16, nmSize = 14, oscSize = 22;
            sb.Append("Excited State |  Multiplicity | Energy (eV) | Energy (cm-1) | Energy (nm) | Oscillator Strength | Transition ").AppendLine().AppendLine();
            foreach (ExcitedState s in e.ExcitedStates)
            {
                string cm = Convert(s.EvEnergy);
                sb.Append(s.ID).Append(string.Concat(Enumerable.Repeat(' ', idSize - s.ID.ToString().Length > 0 ? idSize - s.ID.ToString().Length : 1)))
                    .Append(s.Multiplicity).Append(string.Concat(Enumerable.Repeat(' ', mSize - s.Multiplicity.Length > 0 ? mSize - s.Multiplicity.Length : 1)))
                    .Append(s.EvEnergy.ToString(CultureInfo.InvariantCulture)).Append(string.Concat(Enumerable.Repeat(' ',evSize- s.EvEnergy.ToString().Length  > 0 ? evSize - s.EvEnergy.ToString().Length: 1)))
                    .Append(cm).Append(string.Concat(Enumerable.Repeat(' ', cmSize - cm.Length> 0 ?   cmSize - cm.Length : 1)))
                    .Append(s.NmEnergy.ToString(CultureInfo.InvariantCulture)).Append(string.Concat(Enumerable.Repeat(' ', nmSize- s.NmEnergy.ToString().Length  > 0 ? nmSize- s.NmEnergy.ToString().Length  : 1)))
                    .Append(s.OscillatorStrength.ToString(CultureInfo.InvariantCulture)).Append(string.Concat(Enumerable.Repeat(' ',oscSize- s.OscillatorStrength.ToString().Length  > 0 ? oscSize - s.OscillatorStrength.ToString().Length  : 1)))
                    .Append(s.HLGaps[0].LUMO).Append(" -> ").Append(s.HLGaps[0].HOMO).Append(" ").Append(s.HLGaps[0].EnergyDelta.ToString(CultureInfo.InvariantCulture)).AppendLine();
                for (int i = 1; i < s.HLGaps.Count; i++)
                {
                    sb.Append(string.Concat(Enumerable.Repeat(' ',idSize + mSize+evSize + cmSize+nmSize+oscSize))).Append(s.HLGaps[i].HOMO).Append(" -> ").Append(s.HLGaps[i].LUMO).Append(" ").Append(s.HLGaps[i].EnergyDelta.ToString(CultureInfo.InvariantCulture)).AppendLine();
                    
                }
                sb.AppendLine();
            }
            
            return sb.ToString() ;
        }

        private string Convert(float value)
        {
            return (value * 8065.543937).ToString("0.00", CultureInfo.InvariantCulture);
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
                string p = Path.Combine(path, item.Name + "_standard_orientation.txt");

                MoleculeOrientation orientation = Extractor.ExtractOrientation(item.JobFile, false);
                File.WriteAllText(p, orientation.ToString());
            }
        }

        private void OnLvSelectionChanged(FileBrowserLvSelectMessage obj)
        {
            //Items = obj.DirectoryItemViewModels;

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

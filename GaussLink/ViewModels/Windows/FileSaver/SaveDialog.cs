using GaussLink.Models;
using GaussLink.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GaussLink.ViewModels.Windows.FileSaver
{
    public class SaveDialog:BaseViewModel
    {
        public JobFile JobFile;


        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(name)); }
        }

        private bool content =  false;
        public bool Content
        {
            get { return content; }
            set { content = value;  OnPropertyChanged(nameof(Content)); }
        }

        private bool asJob =  false;
        public bool AsJob
        {
            get { return asJob; }
            set { asJob = value; OnPropertyChanged(nameof(AsJob)); }
        }




    }
}

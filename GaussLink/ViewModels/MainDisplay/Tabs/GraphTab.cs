using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using GaussLink.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Media;

namespace GaussLink.ViewModels.MainDisplay.Tabs
{


    public class GraphTab : TabContent
    {
        string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }


        public ExcitationEnergy ExcitationEnergy;
        public GraphTab(string Name, ExcitationEnergy ExcitationEnergy)
        {
            this.ExcitationEnergy = ExcitationEnergy;
            this.Title = Name;

        }
    }

}


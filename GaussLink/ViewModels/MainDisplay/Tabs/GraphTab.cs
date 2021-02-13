using GaussLink.Models;

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


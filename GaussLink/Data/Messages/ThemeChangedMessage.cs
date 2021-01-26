using GaussLink.ViewModels.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussLink.Data.Messages
{
    public class ThemeChangedMessage
    {
        public ThemeType ThemeType;

        public ThemeChangedMessage(ThemeType ThemeType)
        {
            this.ThemeType = ThemeType;
        }
    }
}

using GaussLink.ViewModels.Themes;

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

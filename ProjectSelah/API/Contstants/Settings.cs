namespace ProjectSelah.API
{
    public static class Settings
    {
        public static bool FirstRun
        {
            get
            {
                return Properties.Settings.Default.firstRun;
            }
            set
            {
                Properties.Settings.Default.firstRun = value;
                Properties.Settings.Default.Save();
            }
        }

        public static bool ForceHeadersUpdate
        {
            get
            {
                return Properties.Settings.Default.forceHeadersUpdate;
            }
            set
            {
                Properties.Settings.Default.forceHeadersUpdate = value;
                Properties.Settings.Default.Save();
            }
        }

        public static int ColorAlpha
        {
            get
            {
                return Properties.Settings.Default.colorAlpha;
            }
            set
            {
                Properties.Settings.Default.colorAlpha = value;
                Properties.Settings.Default.Save();
            }
        }

        public static int PresenterDropShadow
        {
            get
            {
                return Properties.Settings.Default.presenterDropShadow;
            }
            set
            {
                Properties.Settings.Default.presenterDropShadow = value;
                Properties.Settings.Default.Save();
            }
        }

        public static double PresenterFontSize  
        {
            get
            {
                return Properties.Settings.Default.presenterFontSize;
            }
            set
            {
                Properties.Settings.Default.presenterFontSize = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}

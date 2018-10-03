using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectSelah.Views
{
    /// <summary>
    /// Interaction logic for LiveView.xaml
    /// </summary>
    public partial class LiveView : Window
    {
        bool isShown = false;

        public LiveView()
        {
            InitializeComponent();
        }

        public void SetDatacontext(object dataContext)
        {
            presenterControl.DataContext = dataContext;

            presenterControl.CustomFontSizeConstant = 15;
            presenterControl.CustomViewWidth = 1920;
            //presenterControl.CustomViewWidth = 1280;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // put to secondary screen
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        public bool ShowView()
        {
            if (isShown)
                Hide();
            else
            {
                //Show();
                Screen s2 = Screen.AllScreens[Screen.AllScreens.Length > 1 ? 1 : 0];
                presenterControl.CustomViewWidth = s2.WorkingArea.Width;
                System.Drawing.Rectangle r2 = s2.WorkingArea;
                Top = r2.Top;
                Left = r2.Left;
                Show();
                WindowState = WindowState.Maximized;
            }

            

            return isShown = !isShown;
        }
    }
}

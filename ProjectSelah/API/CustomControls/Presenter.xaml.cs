using ProjectSelah.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectSelah.API.CustomControls
{
    /// <summary>
    /// Interaction logic for Presenter.xaml
    /// </summary>
    public partial class Presenter : UserControl
    {
        public double? CustomFontSizeConstant { get; set; }
        public double? CustomViewWidth{ get; set; }

        double boundariesPerc = 0.3;

        public Presenter()
        {
            InitializeComponent();

            bgMedia.MediaEnded += (o, e) => { bgMedia.Position = TimeSpan.FromSeconds(0); };
        }

        public void SetDataContexts()
        {
            bgMedia.DataContext = DataContext;
            displayText.DataContext = DataContext;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeBoundaries();
        }

        private void ResizeBoundaries()
        {
            double marginValue = (ActualWidth * boundariesPerc) / 2;

            if(CustomFontSizeConstant != null)
                displayText.FontSize = ((double)CustomViewWidth / 310) * (double)CustomFontSizeConstant;
        }
    }
}

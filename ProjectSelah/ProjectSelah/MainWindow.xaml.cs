using ProjectSelah.Data_Access;
using ProjectSelah.Models;
using ProjectSelah.ViewModels;
using ProjectSelah.Views;
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

namespace ProjectSelah
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatabaseContext context { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DefaultData.GenerateHeaders();

            DBViewModel<Song> SongVM = new SongViewModel();

            gridRecords.DataContext = SongVM;

            var songView = new SongView();

            btnAdd.Click += (o, ee) =>
            {
                Hide();
                songView.ShowView(ref SongVM);
                Show();
            };

            btnEdit.Click += (o, ee) =>
            {
                Hide();
                songView.ShowView(ref SongVM, false);
                Show();
            };

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}

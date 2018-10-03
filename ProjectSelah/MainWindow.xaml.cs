using ProjectSelah.Data_Access;
using ProjectSelah.Models;
using ProjectSelah.ViewModels;
using ProjectSelah.Views;
using ProjectSelah.API;
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
using ProjectSelah.API.CustomControls;

namespace ProjectSelah
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SongViewModel songVM;
        LineupViewModel lineupVM;
        LyricsViewModel nextLyricsVM;
        LyricsViewModel currentLyricsVM;

        PresenterViewModel presenterVM;

        SongView songView;
        LiveView liveView;

        LiveEditControl liveEdit;

        SolidColorBrush brushNotLive, brushLive;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DefaultData.GenerateHeaders();

            songVM = new SongViewModel();

            liveView = new LiveView();

            gridRecords.DataContext = songVM;

            fieldSongSearch.DataContext = songVM;

            songView = new SongView(songVM);

            btnAdd.Click += (o, ee) =>
            {
                songVM.CurrentItem = new Song();

                songView.ShowView();

                Show();
            };

            btnEdit.Click += (o, ee) =>
            {
                songView.ShowView(false);

                Show();
            };

            lineupVM = new LineupViewModel();

            currentLyricsVM = new LyricsViewModel();

            nextLyricsVM = new LyricsViewModel();

            lineupVM.CurrentItemCallback = i =>
            {
                if (i != null)
                    nextLyricsVM.Song = i;
                else
                    nextLyricsVM.Song = null;
            };

            gridLineup.DataContext = lineupVM;

            gridNextLyrics.DataContext = nextLyricsVM;

            gridCurrentLyrics.DataContext = currentLyricsVM;

            presenterVM = new PresenterViewModel();

            currentLyricsVM.CurrentItemCallback = i =>
            {
                presenterVM.CurrentItem = currentLyricsVM.CurrentItem;
            };

            lblCurrentSongTitle.DataContext = currentLyricsVM;

            liveView.SetDatacontext(presenterVM);

            presenterControlRefl.DataContext = presenterVM;

            presenterControlRefl.SetDataContexts();

            btnClearLiveView.DataContext = presenterVM;

            btnBlackLiveView.DataContext = presenterVM;

            liveEdit = new LiveEditControl();

            gridMain.Children.Add(liveEdit);

            liveEdit.Visibility = Visibility.Collapsed;

            KeyBinding keyBinding = new KeyBinding(
                ApplicationCommands.NotACommand,
                Key.E, ModifierKeys.Control);

            gridNextLyrics.InputBindings.Add(keyBinding);
            gridCurrentLyrics.InputBindings.Add(keyBinding);

            gridNextLyrics.KeyDown += LyricsControl_KeyDown;
            gridCurrentLyrics.KeyDown += LyricsControl_KeyDown;

            brushNotLive = new SolidColorBrush(("#FF00A300").ToColor());
            brushLive = new SolidColorBrush(("#FFE82323").ToColor());

            gridRecords.Focus();
        }

        private void LyricsControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                if (e.Key == Key.E)
                {
                    var datacontext = ((LyricsViewModel)((FrameworkElement)sender).DataContext);

                    var grid = (DataGrid)sender;

                    // show live edit control over grid
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();

            //songVM.Dispose();
            //lineupVM.Dispose();
            //nextLyricsVM.Dispose();
            //currentLyricsVM.Dispose();
        }

        private void ContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lineupVM.AddToItems.Execute(songVM.CurrentItem);
        }

        private void BtnLineupRemove_Click(object sender, RoutedEventArgs e)
        {
            gridLineup.Focus();
        }

        private void NextLyrics_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            currentLyricsVM.Song = nextLyricsVM.Song;
            currentLyricsVM.CurrentItem = nextLyricsVM.CurrentItem;
            lineupVM.Next();
        }

        private void BtnShowLiveView_Click(object sender, RoutedEventArgs e)
        {
            if (liveView.ShowView())
                btnShowLiveView.Background = brushLive;
            else
                btnShowLiveView.Background = brushNotLive;
        }
    }
}
using ProjectSelah.API;
using ProjectSelah.Models;
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
using System.Windows.Shapes;

namespace ProjectSelah.Views
{
    /// <summary>
    /// Interaction logic for SongView.xaml
    /// </summary>
    public partial class SongView : Window
    {

        Song OldData { get; set; } = default(Song);

        Song internal_Data { get; set; }
        public Song Data
        {
            get { return internal_Data; }
            set
            {
                internal_Data = value;
                FormExtensions.NotifyPropertyChanged(this);
            }
        }

        public Command NewCmd { get; set; }
        public Command SaveCmd { get; set; }
        public Command SaveChCmd { get; set; }
        public Command DeleteCmd { get; set; }
        public Command RefCmd { get; set; }

        DBViewModel<Song> SongVM = null;

        public SongView()
        {
            InitializeComponent();

            NewCmd = new Command(
                o =>
                {
                    Data.Name = "";
                    Data.Artist = "";
                    Data.Lyrics.Clear();

                    OldData = default(Song);
                }
            );

            SaveCmd = new Command(
                o =>
                {
                    SongVM.Add(Data);
                    OldData = Data;

                    SetButtonMode(1);
                    OnFieldsChange(this, null);

                },
                () =>
                {
                    return
                        OldData == default(Song) &&
                        Data.Name != "" && Data.Artist != "" && Data.Lyrics != null && Data.Lyrics.Count(ly => ly.Stanza != "") > 0;
                }
            );

            SaveChCmd = new Command(
                o =>
                {
                    SongVM.Update(Data);

                    OldData = Data;

                    OnFieldsChange(this, null);

                },
                () =>
                {
                    return
                        OldData != default(Song) &&
                        (Data.Name != "" && Data.Artist != "" && Data.Lyrics != null && Data.Lyrics.Count(ly => string.IsNullOrEmpty(ly.Stanza)) == 0) &&
                        (OldData.Name != Data.Name || OldData.Artist != Data.Artist || (Data.Lyrics != null && !OldData.Lyrics.CompareTo(Data.Lyrics)));
                }
            );

            DeleteCmd = new Command(
                o =>
                {
                    SongVM.Delete(Data);

                    Close();
                },
                () =>
                {
                    return OldData != default(Song) && Data != null;
                }
            );

            RefCmd = new Command(
                o =>
                {
                    Data.Update(OldData);
                },
                () =>
                {
                    return
                        OldData != default(Song) &&
                        (Data.Name != "" && Data.Artist != "" && Data.Lyrics != null && Data.Lyrics.Count(ly => string.IsNullOrEmpty(ly.Stanza)) == 0) &&
                        (OldData.Name != Data.Name || OldData.Artist != Data.Artist || (Data.Lyrics != null && !OldData.Lyrics.CompareTo(Data.Lyrics)));
                }
            );

            fieldName.TextChanged += OnFieldsChange;
            fieldArtist.TextChanged += OnFieldsChange;
            fieldLyrics.TextChanged += OnFieldsChange;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            fieldName.DataContext = this;
            fieldArtist.DataContext = this;
            fieldLyrics.DataContext = this;

            btnSaveNew.DataContext = this;
            btnSaveChanges.DataContext = this;
            btnDelete.DataContext = this;
            btnRefresh.DataContext = this;
        }

        public void ShowView(ref DBViewModel<Song> songVM, bool add = true)
        {
            SongVM = songVM;

            if (!add)
            {
                OldData = songVM.CurrentItem.Clone();
                if (Data == null)
                    Data = new Song();
                Data.Update(OldData);
            }
            else
            {
                Data = new Song();
            }

            SetButtonMode(add ? 0 : 1);

            ShowDialog();
        }

        private void SetButtonMode(int mode = 1)
        {
            // mode = 1 is edit
            // mode = 0 is new

            var isEditMode = mode == 1;

            btnSaveNew.Visibility = isEditMode ? Visibility.Collapsed : Visibility.Visible;
            btnSaveChanges.Visibility = !isEditMode ? Visibility.Collapsed : Visibility.Visible;
            btnDelete.Visibility = !isEditMode ? Visibility.Collapsed : Visibility.Visible;
            btnRefresh.Visibility = !isEditMode ? Visibility.Collapsed : Visibility.Visible;
        }

        private void OnFieldsChange(object sender, TextChangedEventArgs e)
        {
            SaveCmd.RaiseCanExecuteChanged();
            SaveChCmd.RaiseCanExecuteChanged();
            DeleteCmd.RaiseCanExecuteChanged();
            RefCmd.RaiseCanExecuteChanged();
        }
    }
}

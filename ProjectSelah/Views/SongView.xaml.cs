using Microsoft.Win32;
using ProjectSelah.API;
using ProjectSelah.Models;
using ProjectSelah.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        Song Internal_Data { get; set; }
        public Song Data
        {
            get { return Internal_Data; }
            set
            {
                Internal_Data = value;
                Notifyable.NotifyPropertyChanged(this);
            }
        }

        private Song CurrentItem
        {
            get { return SongVM.CurrentItem; }
            set
            {
                SongVM.CurrentItem = value;
            }
        }

        private bool DataNotEmpty
        {
            get
            {
                return Data.Name != "" && Data.Lyrics.Count(ly => ly.Stanza != "") > 0;
            }
        }

        public Command NewCmd { get; set; }
        public Command SaveCmd { get; set; }
        public Command SaveChCmd { get; set; }
        public Command DeleteCmd { get; set; }
        public Command RefCmd { get; set; }
        public Command ExportCmd { get; set; }

        SongViewModel SongVM { get; set; }

        public SongView(SongViewModel songVM)
        {
            InitializeComponent();

            Data = new Song();

            SongVM = songVM;

            NewCmd = new Command(
                o =>
                {
                    Data = new Song();
                }
            );

            SaveCmd = new Command(
                o =>
                {
                    SongVM.Add(Data);

                    SetButtonMode(1);

                    OnFieldsChange(this, null);

                },
                () =>
                {
                    return
                        (!string.IsNullOrEmpty(Data.Name)) &&
                        (Data.Lyrics != null &&
                        Data.Lyrics.AsText() != "_$ENDOFTEXT$_");
                }
            );

            SaveChCmd = new Command(
                o =>
                {
                    SongVM.Update(Data);

                    OnFieldsChange(this, null);
                },
                () =>
                {
                    //if (OldData != default(Song) && DataNotEmpty &&
                    //    (OldData.Name != Data.Name || OldData.Artist != Data.Artist))
                    //    return true;

                    //if (fieldLyrics.IsDirty)
                    //    return (OldData != null && fieldLyrics.Text != OldData.Lyrics.AsText());

                    //return false;

                    return !CurrentItem.IsEqualTo(Data);
                }
            );

            DeleteCmd = new Command(
                o =>
                {
                    SongVM.Delete();

                    Close();
                },
                () =>
                {
                    return Data != default(Song) && CurrentItem != default(Song);
                }
            );

            RefCmd = new Command(
                o =>
                {
                    Data.Update(CurrentItem);
                },
                () =>
                {
                    //if (OldData != default(Song) && DataNotEmpty &&
                    //    (OldData.Name != Data.Name || OldData.Artist != Data.Artist))
                    //    return true;

                    //if (fieldLyrics.IsDirty)
                    //    return (OldData != null && fieldLyrics.Text != OldData.Lyrics.AsText());

                    return !CurrentItem.IsEqualTo(Data);
                }
            );

            ExportCmd = new Command(
                o =>
                {
                    //if (OldData != default(Song) && DataNotEmpty &&
                    //       (OldData.Name != Data.Name || OldData.Artist != Data.Artist ||
                    //       (OldData != null && fieldLyrics.Text != OldData.Lyrics.AsText())))
                    //{
                    //    if (MessageBox.Show(
                    //        "Continuing export will export the saved data instead of the current one. Continue?",
                    //        "There are unsaved changes",
                    //        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    //    {
                    //        SongVM.Export();
                    //    }
                    //}
                    //else
                    //{
                    //    SongVM.Export();
                    //}
                }
            );
        }

        private void Window_Closing(object sender, CancelEventArgs e)
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
            btnExport.DataContext = this;

            fieldName.TextChanged += OnFieldsChange;
            fieldArtist.TextChanged += OnFieldsChange;
            fieldLyrics.TextChanged += OnFieldsChange;
        }

        public void ShowView(bool add = true)
        {
            fieldLyrics.RefreshHeaders();

            Data.Update(CurrentItem.Clone());

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
            btnExport.Visibility = !isEditMode ? Visibility.Collapsed : Visibility.Visible;
        }

        private void OnFieldsChange(object sender, TextChangedEventArgs e)
        {
            SaveCmd.RaiseCanExecuteChanged();
            SaveChCmd.RaiseCanExecuteChanged();
            DeleteCmd.RaiseCanExecuteChanged();
            RefCmd.RaiseCanExecuteChanged();
            ExportCmd.RaiseCanExecuteChanged();
        }

        private void Dispose()
        {
            Data = default(Song);
        }
    }
}

using Microsoft.Win32;
using ProjectSelah.API;
using ProjectSelah.Data_Access;
using ProjectSelah.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectSelah.ViewModels
{
    public class LineupViewModel : ListViewModel<Song>
    {
        public Command AddToItems { get; set; }
        public Command Remove { get; set; }
        public Command MoveUp { get; set; }
        public Command MoveDown { get; set; }
        public Command Clear { get; set; }

        public Command Open { get; set; }
        public Command Save { get; set; }
        public Command New { get; set; }

        string Internal_Filename { get; set; }
        public string Filename
        {
            get { return Internal_Filename; }
            set
            {
                Internal_Filename = value;
                NotifyPropertyChanged();
            }
        }

        OpenFileDialog diag;

        bool isDirty;

        public LineupViewModel() : base()
        {
            diag = new OpenFileDialog();

            AddToItems = new Command(
                i =>
                {
                    if (i != default(Song) && i is Song)
                        Add(i as Song);
                }
            );

            Remove = new Command(
                i =>
                {
                    int currIndex = ItemsList.IndexOf(CurrentItem);

                    if (CurrentItem != default(Song))
                        ItemsList.Remove(CurrentItem);

                    if (ItemsList.Count > 0)
                    {
                        if (ItemsList.Count == currIndex)
                        {
                            currIndex--;
                        }

                        CurrentItem = ItemsList[currIndex];
                    }
                },
                () =>
                {
                    return CurrentItem != null;
                }
            );

            MoveUp = new Command(
                i =>
                {
                    if (CurrentItem != default(Song))
                        ItemsList.Remove(CurrentItem);
                },
                () =>
                {
                    return ItemsList.First() != CurrentItem;
                }
            );

            MoveDown = new Command(
                i =>
                {
                    if (CurrentItem != default(Song))
                        ItemsList.Remove(CurrentItem);
                },
                () =>
                {
                    return ItemsList.Last() != CurrentItem;
                }
            );

            Clear = new Command(
                i =>
                {
                    ItemsList.Clear();
                },
                () =>
                {
                    return ItemsList.Count > 0;
                }
            );

            Open = new Command(
                LoadLineupFile,
                () =>
                {
                    return ItemsList.Count > 0;
                }
            );

            Save = new Command(
                i =>
                {
                    ItemsList.Clear();
                },
                () =>
                {
                    return ItemsList.Count > 0 && isDirty;
                }
            );

            New = new Command(
                i =>
                {
                    ItemsList.Clear();
                    Filename = "";
                },
                () =>
                {
                    return ItemsList.Count > 0;
                }
            );

            ItemsList.CollectionChanged += (o, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add &&
                    ItemsList.Count == 1)
                    CurrentItem = ItemsList[0];

                isDirty = true;
            };
        }

        public void Next()
        {
            int currIdx = ItemsList.IndexOf(CurrentItem);
            if (ItemsList.Count - 1 != currIdx)
                currIdx++;
            CurrentItem = ItemsList[currIdx];
        }

        private void LoadLineupFile(object i)
        {
            if (diag.ShowDialog() == true)
            {
                Filename = Path.GetFileNameWithoutExtension(diag.FileName);

                string[] lines = File.ReadAllLines(Filename);

                ItemsList.Clear();

                using (var c = new DatabaseContext())
                {
                    List<Song> songs = c.Songs.Include("Lyrics").ToList();

                    foreach (var line in lines)
                    {
                        if (line[0] == '~')
                        {
                            var newLine = line.Replace("~", "").Split('|');

                            var song = songs.FirstOrDefault(data => data.Id == newLine[0] && data.Name == newLine[1]);

                            if (song != default(Song))
                                ItemsList.Add(song);
                        }
                    } 
                }
            }
        }
    }
}

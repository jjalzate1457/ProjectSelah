using Microsoft.Win32;
using ProjectSelah.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectSelah.API.Base_Classes
{
    public abstract class FileClass
    {
        protected string Filename { get; set; }

        public abstract void Write();

        protected virtual bool? GetPath()
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                FileName = Filename,
                InitialDirectory = @"%userprofile%\Documents",
                Filter = @"Txt files|*.txt"
            };

            bool result = (bool)sfd.ShowDialog();
            if (result)
                Filename = sfd.FileName;

            return result;
        }
    }

    public class SongFile : FileClass
    {
        Song song = null;

        public SongFile(Song song)
        {
            this.song = song;

            Filename = song.Name + "_" + song.Id + ".txt";
        }

        public override void Write()
        {
            try
            {
                if ((bool)GetPath())
                {
                    using (StreamWriter w = new StreamWriter(Filename))
                    {
                        Header header = null;

                        List<Lyrics> lyrics = song.Lyrics.OrderBy(i => i.Order).ToList();

                        foreach (Lyrics item in lyrics)
                        {
                            if (header == null || header.Id != item.Header.Id)
                            {
                                header = item.Header;
                                w.WriteLine(header.Name);
                            }

                            if (lyrics.Last().Id == item.Id)
                            {
                                w.Write(item.Stanza);
                            }
                            else
                            {
                                w.WriteLine(item.Stanza);
                                w.WriteLine();
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public class LineupFile : FileClass
    {
        private IList<Song> lineup = null;

        public LineupFile(IEnumerable<Song> lineup)
        {
            foreach (var item in lineup)
            {
                this.lineup.Add(item);
            }
        }

        public override void Write()
        {
            if ((bool)GetPath())
            {
                using (StreamWriter w = new StreamWriter(Filename))
                {
                    w.WriteLine(Filename);
                    w.WriteLine(DateTime.Now);

                    foreach (Song item in lineup)
                    {
                        w.WriteLine(item.Name + "_" + item.Id);
                    }
                }
            }
        }
    }
}
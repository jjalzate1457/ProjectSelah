using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectSelah.Models
{
    public class Song : Model, IEquatable<Song>
    {
        string db_Artist { get; set; }
        [Column(name: "Artist")]
        public string Artist
        {
            get { return db_Artist; }
            set
            {
                db_Artist = value;
                NotifyPropertyChanged("Artist");
            }
        }

        ObservableCollection<Lyrics> internal_Lyrics { get; set; }
        public virtual ObservableCollection<Lyrics> Lyrics
        {
            get { return internal_Lyrics; }
            set
            {
                internal_Lyrics = value;
                NotifyPropertyChanged("Lyrics");
            }
        }

        public bool Equals(Song other)
        {
            var lyricsFlag = true;

            if (other.Lyrics != null && Lyrics != null&&
                other.Lyrics.Count == Lyrics.Count)
            {
                for (var a =0; a < Lyrics.Count; a++)
                {
                    if(!other.Lyrics[a].Equals(Lyrics[a]))
                    {
                        lyricsFlag = false;
                        break;
                    }
                }
            }

            return
                other.Id == Id &&
                other.Name == Name &&
                other.Artist == Artist &&
                lyricsFlag;
        }
    }
}

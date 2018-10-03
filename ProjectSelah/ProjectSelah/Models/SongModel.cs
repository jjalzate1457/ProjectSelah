using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectSelah.Models
{
    public class Song : Model
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
        public ObservableCollection<Lyrics> Lyrics
        {
            get { return internal_Lyrics; }
            set
            {
                internal_Lyrics = value;
                NotifyPropertyChanged("Lyrics");
            }
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectSelah.Models
{
    public class Lyrics : Model
    {
        string db_Name { get; set; }
        [Column(name: "Stanza")]
        public string Stanza
        {
            get { return db_Name; }
            set
            {
                db_Name = value;
                NotifyPropertyChanged("Stanza");
            }
        }


        [ForeignKey("Song")]
        public string SongId { get; set; }
        [ForeignKey("SongId")]
        public virtual Song Song { get; set; }

        [ForeignKey("Header")]
        public string HeaderId { get; set; }
        [ForeignKey("HeaderId")]
        public virtual Header Header { get; set; }

        public Lyrics()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

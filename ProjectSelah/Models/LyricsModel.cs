using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectSelah.Models
{
    public class Lyrics : Model, IEquatable<Lyrics>
    {
        string db_Name { get; set; }
        [Column(name: "Stanza")]
        public string Stanza
        {
            get { return db_Name.Trim(); }
            set
            {
                db_Name = value.Trim();
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

        public bool Equals(Lyrics other)
        {
            return
                other.Id == Id &&
                other.Name == Name &&
                other.Stanza == Stanza &&
                other.HeaderId == HeaderId &&
                other.Header == Header;
        }
    }
}

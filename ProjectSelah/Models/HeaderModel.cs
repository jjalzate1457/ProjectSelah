using ProjectSelah.API;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;

namespace ProjectSelah.Models
{
    public class Header : Model, IEquatable<Header>
    {
        [Column(name: "highlight")]
        string db_Color { get; set; }
        public string Color
        {
            get { return (db_Color); }
            set
            {
                db_Color = (value);
                NotifyPropertyChanged();
            }
        }

        public Color Highlight
        {
            get { return ColorFunctions.ToColor(Color); }
            set { Color = ColorFunctions.ToString(value); NotifyPropertyChanged(); }
        }

        [Column(name: "isdefault")]
        bool db_IsDefault { get; set; }
        public bool IsDefault
        {
            get { return db_IsDefault; }
            set
            {
                db_IsDefault = value;
                NotifyPropertyChanged();
            }
        }

        public bool Equals(Header other)
        {
            return
                other.Id == Id &&
                other.Name == Name &&
                other.Color == Color &&
                other.IsDefault == IsDefault;
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;

namespace ProjectSelah.Models
{
    public class Header : Model
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
            get { return ToColor(Color); }
            set { Color = ToString(value); NotifyPropertyChanged(); }
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


        string ToString(Color Color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", Color.A, Color.R, Color.G, Color.B);
        }

        Color ToColor(string Hex)
        {
            if (Hex == null) return Colors.White;
            return (Color)ColorConverter.ConvertFromString("#" + Hex.Replace("#", ""));
        }
    }
}

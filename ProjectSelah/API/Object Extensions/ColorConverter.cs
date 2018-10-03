using System.Windows.Media;

namespace ProjectSelah.API
{
    public static class ColorFunctions
    {
        public static string ToString(this Color Color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", Color.A, Color.R, Color.G, Color.B);
        }

        public static Color ToColor(this string Hex)
        {
            if (Hex == null) return Colors.White;
            return (Color)ColorConverter.ConvertFromString("#" + Hex.Replace("#", ""));
        }
    }
}

using ProjectSelah.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSelah.API
{
    public static class ObjectExtensions
    {
        public static bool CompareTo(this IList<Lyrics> collection1, IList<Lyrics> collection2)
        {
            if (collection1.Count() != collection2.Count())
                return false;

            for(int a = 0; a < collection1.Count(); a++)
            {
                if (collection1[a].Stanza != collection2[a].Stanza)
                    return false;
            }

            return true;
        }

        public static void Update<T>(this T forUpdating, T newData)
        {
            var typeofT = typeof(T);

            foreach (var prop in typeof(T).GetProperties())
            {
                var name = prop.Name;
                var value = typeofT.GetProperty(name).GetValue(newData);
                typeofT.GetProperty(name).SetValue(forUpdating, value);
            }

            if (forUpdating is Notifyable)
                (forUpdating as Notifyable).NotifyPropertyChanged("");
        }
    }
}

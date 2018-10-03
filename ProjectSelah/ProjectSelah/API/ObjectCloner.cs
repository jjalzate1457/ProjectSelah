using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSelah.API
{
    public static class ObjectFunctions
    {
        public static T Clone<T>(this T forCloning)
            where T : class, new()
        {
            var clone = new T();
            var typeofT = typeof(T);

            foreach (var prop in typeof(T).GetProperties())
            {
                var name = prop.Name;
                var value = typeofT.GetProperty(name).GetValue(forCloning);
                typeofT.GetProperty(name).SetValue(clone, value);
            }

            return clone;
        }

        public static void Update<T>(ref T forUpdating, T newData)
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

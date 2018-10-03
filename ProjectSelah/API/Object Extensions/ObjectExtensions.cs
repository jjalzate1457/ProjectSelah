using ProjectSelah.Models;
using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace ProjectSelah.API
{
    public static class ObjectExtensions
    {
        public static T Clone<T>(this T forCloning)
            where T : class, new()
        {
            var clone = new T();
            var typeofT = typeof(T);

            foreach (var prop in typeofT.GetProperties())
            {
                var name = prop.Name;
                var value = typeofT.GetProperty(name).GetValue(forCloning);
                typeofT.GetProperty(name).SetValue(clone, value);
            }

            return clone;
        }

        public static void Update<T>(this T forUpdating, T newData)
        {
            var typeofT = typeof(T);

            foreach (var prop in typeofT.GetProperties())
            {
                var name = prop.Name;
                var value = typeofT.GetProperty(name).GetValue(newData);
                typeofT.GetProperty(name).SetValue(forUpdating, value);
            }

            if (forUpdating is Notifyable)
                (forUpdating as Notifyable).NotifyPropertyChanged("");
        }

        public static bool IsEqualTo<T>(this T forUpdating, T data)
        {
            Type typeofT = typeof(T);

            foreach (System.Reflection.PropertyInfo prop in typeofT.GetProperties())
            {
                string name = prop.Name;

                Type type1 = typeofT.GetProperty(name).PropertyType;
                Type type2 = typeofT.GetProperty(name).PropertyType;

                var value1 = typeofT.GetProperty(name).GetValue(forUpdating);
                var value2 = typeofT.GetProperty(name).GetValue(data);

                if (value1 != null && value2 != null && value1.GetType() == typeof(ObservableCollection<Lyrics>))
                {
                    if (!(value1 as ObservableCollection<Lyrics>).CompareTo(value2 as ObservableCollection<Lyrics>))
                        return false;
                }
                else if (!Equals(value1, value2))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

namespace ProjectSelah.API
{
    public static class ObjectFunctions
    {
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

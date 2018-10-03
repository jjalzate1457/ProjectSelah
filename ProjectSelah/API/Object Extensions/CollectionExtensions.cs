using System.Data.Entity;
using System.Linq;

namespace ProjectSelah.API
{
    public static class CollectionExtensions
    {
        public static bool Exists<T>(this DbSet<T> dbset, T data)
            where T : class, new()
        {
            return (from item in dbset
                    where item == data
                    select item).FirstOrDefault() == default(T);
        }
    }
}

using ProjectSelah.Data_Access;
using ProjectSelah.Models;
using System;

namespace ProjectSelah.ViewModels
{
    public class DBViewModel<T> : ListViewModel<T>, IDisposable
        where T : Model, new()
    {
        protected DatabaseContext Context { get; set; }

        public DBViewModel()
        {
            Context = new DatabaseContext();
            PopulateFromDB();
        }

        public virtual void PopulateFromDB()
        {
            ItemsList.Clear();

            foreach (var item in Context.Set<T>())
            {
                ItemsList.Add(item);
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}

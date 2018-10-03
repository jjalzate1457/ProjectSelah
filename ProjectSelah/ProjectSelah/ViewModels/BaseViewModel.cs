using ProjectSelah.API;
using ProjectSelah.Data_Access;
using ProjectSelah.Models;
using System;
using System.Data.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjectSelah.ViewModels
{
    public class BaseViewModel<T> : Notifyable
        where T : Model, new()
    {
        T internal_CurrentItem { get; set; }
        public T CurrentItem
        {
            get { return internal_CurrentItem; }
            set
            {
                internal_CurrentItem = value;
                NotifyPropertyChanged();
            }
        }


        public ObservableCollection<T> Items { get; set; }

        public BaseViewModel()
        {
            Items = new ObservableCollection<T>();
        }

        public virtual void Add(T data)
        {
            Items.Add(data);
        }

        public virtual void Update(T data)
        {
            var forUpdate = Items.FirstOrDefault(m => m.Id == data.Id);
            if (forUpdate != default(T))
                forUpdate.Update(data);
        }

        public virtual void Delete(T data)
        {
            Items.Remove(data);
        }
    }

    public class DBViewModel<T> : BaseViewModel<T>
        where T : Model, new()
    {
        public DBViewModel()
        {
            PopulateFromDB();
        }

        public virtual void PopulateFromDB()
        {
            using (var context = new DatabaseContext())
            {
                foreach (var item in context.Set<T>())
                {
                    Items.Add(item);
                }
            }
        }

        public override void Add(T data)
        {
            using (var context = new DatabaseContext())
            {
                context.Set<T>().Add(data);

                foreach (var prop in typeof(T).GetProperties())
                {
                    var name = prop.Name;

                    var value = typeof(T).GetProperty(name).GetValue(data);

                    if (value is IEnumerable<Lyrics> && (value as IEnumerable<Lyrics>).Count() > 0)
                    {
                        var modelColl = (from item in value as IEnumerable<Lyrics>
                                         select item);

                        var dbSet1 = context.Set(modelColl.First().GetType());
                        var dbSet2 = context.Set(modelColl.First().Header.GetType());

                        foreach (var modelItem in modelColl)
                        {
                            //dbSet1.Attach(modelItem);
                            dbSet2.Attach(modelItem.Header);
                        }
                    }
                }

                context.SaveChanges();

                base.Add(data);
            }
        }

        public override void Update(T data)
        {
            using (var context = new DatabaseContext())
            {
                var forUpdate = context.Set<T>().FirstOrDefault(i => i.Id == data.Id);

                if (forUpdate != default(T))
                {
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        var name = prop.Name;

                        var value = typeof(T).GetProperty(name).GetValue(data);

                        if (value is IEnumerable<Lyrics> && (value as IEnumerable<Lyrics>).Count() > 0)
                        {
                            var modelColl = (from item in value as IEnumerable<Lyrics>
                                             select item);

                            var dbSet = context.Set(modelColl.First().GetType());
                            var dbSet2 = context.Set(modelColl.First().Header.GetType());

                            foreach (var modelItem in modelColl)
                            {
                                dbSet.Attach(modelItem);
                                dbSet2.Attach(modelItem.Header);
                            }
                        }
                    }

                    forUpdate.Update(data);

                    context.SaveChanges();

                    base.Update(data);
                }
            }
        }

        public override void Delete(T data)
        {
            using (var context = new DatabaseContext())
            {
                var dbset = context.Set<T>();

                var forDeletion = (from item in dbset
                                   where item.Id == data.Id
                                   select item).FirstOrDefault();

                if (forDeletion != default(T))
                {
                    context.Set<T>().Remove(forDeletion);
                    context.SaveChanges();

                    base.Delete(data);
                }
            }
        }
    }

    public class SongViewModel : DBViewModel<Song>
    {
        public override void PopulateFromDB()
        {
            var context = new DatabaseContext();
            foreach (var item in context.Songs.Include("Lyrics").ToList())
            {
                Items.Add(item.Clone());
            }
        }
    }
}

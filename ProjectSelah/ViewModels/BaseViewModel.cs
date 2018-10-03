using ProjectSelah.API;
using ProjectSelah.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace ProjectSelah.ViewModels
{
    public class BaseViewModel<T> : Notifyable
        where T : Model, new()
    {
        T Internal_CurrentItem { get; set; }
        public T CurrentItem
        {
            get { return Internal_CurrentItem; }
            set
            {
                Internal_CurrentItem = value;
                NotifyPropertyChanged();
                CurrentItemCallback?.Invoke(value);
            }
        }

        public Action<T> CurrentItemCallback { get; set; }
    }

    public class ListViewModel<T> : BaseViewModel<T>
        where T : Model, new()
    {
        public ObservableCollection<T> ItemsList { get; set; }
        public ICollectionView Items { get; set; }

        protected List<Predicate<T>> Criteria { get; set; }

        public ListViewModel()
        {
            ItemsList = new ObservableCollection<T>();

            ItemsList.CollectionChanged += (o, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    CurrentItem = (T)e.NewItems[0];
                }
            };

            Items = CollectionViewSource.GetDefaultView(ItemsList);

            ReorderList("Order", "Name");
        }

        public void ReorderList(params string[] properties)
        {
            ReorderList(ListSortDirection.Ascending, properties);
        }

        public void ReorderList(ListSortDirection direction, params string[] properties)
        {
            Items.SortDescriptions.Clear();
            foreach (var property in properties)
                Items.SortDescriptions.Add(new SortDescription(property, direction));
        }

        public virtual void Add()
        {
            ItemsList.Add(CurrentItem);
        }

        public virtual void Add(T Data)
        {
            ItemsList.Add(Data);
        }

        public virtual void Delete()
        {
            ItemsList.Remove(CurrentItem);
        }
    }
}

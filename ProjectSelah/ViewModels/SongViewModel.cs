using ProjectSelah.API;
using ProjectSelah.API.Base_Classes;
using ProjectSelah.Models;
using System.Linq;

namespace ProjectSelah.ViewModels
{
    public class SongViewModel : DBViewModel<Song>
    {
        string Internal_SearchString { get; set; } = "";
        public string SearchString
        {
            get { return Internal_SearchString; }
            set
            {
                Internal_SearchString = value;
                NotifyPropertyChanged();
                Items.Refresh();
            }
        }

        public SongViewModel()
        {
            Criteria = new System.Collections.Generic.List<System.Predicate<Song>>
            {
                data => { return (data as Song).Name.Contains(Internal_SearchString); },
                data => { return (data as Song).Artist != null && (data as Song).Artist.Contains(Internal_SearchString); }
            };

            Items.Filter = i =>
            {
                Song data = i as Song;
                return Criteria.Any(x => x(data));
            };
        }

        public override void PopulateFromDB()
        {
            ItemsList.Clear();

            foreach (var item in Context.Songs.Include("Lyrics"))
            {
                ItemsList.Add(item);
            }
        }

        public new void Add(Song Data)
        {
            Song temp = Data.Clone();

            CurrentItem = Data;

            foreach (var item in CurrentItem.Lyrics)
            {
                item.Header = null;
            }

            Context.Songs.Add(CurrentItem);
            Context.SaveChanges();

            for (int a = 0; a < temp.Lyrics.Count; a++)
            {
                CurrentItem.Lyrics[a].Header = temp.Lyrics[a].Header;
            }

            base.Add();
        }

        public void Update(Song Data)
        {
            Song temp = Data.Clone();

            CurrentItem.Update(Data);

            foreach (var item in CurrentItem.Lyrics)
            {
                item.Header = null;
            }

            Context.SaveChanges();

            for(int a  = 0; a < temp.Lyrics.Count; a++)
            {
                CurrentItem.Lyrics[a].Header = temp.Lyrics[a].Header;
            }
        }

        public override void Delete()
        {
            if (CurrentItem != default(Song))
            {
                Context.Songs.Remove(CurrentItem);
                Context.SaveChanges();

                base.Delete();
            }
        }

        public void Export()
        {
            SongFile file = new SongFile(CurrentItem);

            file.Write();
        }
    }
}

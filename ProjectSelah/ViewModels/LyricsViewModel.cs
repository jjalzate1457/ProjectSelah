using ProjectSelah.Models;

namespace ProjectSelah.ViewModels
{
    public class LyricsViewModel : ListViewModel<Lyrics>
    {
        Song Internal_Song { get; set; }
        public Song Song
        {
            get { return Internal_Song; }
            set
            {
                Internal_Song = value;

                ItemsList.Clear();

                if (value != null)
                {
                    foreach (var item in value.Lyrics)
                        ItemsList.Add(item);
                }

                NotifyPropertyChanged();
            }
        }

        public LyricsViewModel()
        {
            
        }
    }
}

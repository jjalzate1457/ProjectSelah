using ProjectSelah.API;
using ProjectSelah.Models;

namespace ProjectSelah.ViewModels
{
    public class PresenterViewModel : BaseViewModel<Lyrics>
    {
        string Internal_BackgroundMedia { get; set; }
        public string BackgroundMedia
        {
            get { return Internal_BackgroundMedia; }
            set
            {
                Internal_BackgroundMedia = value;
                NotifyPropertyChanged();
            }
        }

        PresenterState Internal_State { get; set; }
        public PresenterState State
        {
            get { return Internal_State; }
            set
            {
                Internal_State = value;
                NotifyPropertyChanged();
            }
        }

        int Internal_DropShadow { get; set; }
        public int DropShadow
        {
            get { return Internal_DropShadow; }
            set
            {
                Internal_DropShadow = value;
                NotifyPropertyChanged();
            }
        }

        public Command ClearScreenCmd { get; set; }
        public Command BlackScreenCmd { get; set; }

        public PresenterViewModel()
        {
            State = PresenterState.Normal;

            ClearScreenCmd = new Command(
            i =>
            {
                if (State == PresenterState.ClearScreen)
                    State = PresenterState.Normal;
                else
                    State = PresenterState.ClearScreen;
            },
            () =>
            {
                return State != PresenterState.BlackScreen &&
                    State != PresenterState.ClearBlackScreen;
            });

            BlackScreenCmd = new Command(
            i =>
            {
                if (State == PresenterState.Normal)
                {
                    State = PresenterState.BlackScreen;
                }
                else if (State == PresenterState.ClearScreen)
                {
                    State = PresenterState.ClearBlackScreen;
                }
                else if (State == PresenterState.BlackScreen)
                {
                    State = PresenterState.Normal;
                }
                else if (State == PresenterState.ClearBlackScreen)
                {
                    State = PresenterState.ClearScreen;
                }
            });

            DropShadow = Settings.PresenterDropShadow;

            BackgroundMedia = Directories.VideoDir +  "streaks.mp4";
        }
    }
}

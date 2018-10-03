using ProjectSelah.API;
using ProjectSelah.Models;

namespace ProjectSelah.ViewModels
{
    public class ViewViewModel<T> : BaseViewModel<T>
        where T : Model, new()
    {
        public ViewState Internal_ViewState { get; set; }
        public ViewState ViewState
        {
            get { return Internal_ViewState; }
            protected set
            {
                Internal_ViewState = value;
                NotifyPropertyChanged();
            }
        }

        public Command NewCmd { get; set; }
        public Command SaveCmd { get; set; }
        public Command UpdateCmd { get; set; }
        public Command DeleteCmd { get; set; }
        public Command RefreshCmd { get; set; }

        public ViewViewModel()
        {
            NewCmd = new Command(
            Data => {
                CurrentItem = new T();
                ViewState = ViewState.New;
            });
        }
    }
}

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectSelah.API
{
    public class Notifyable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}

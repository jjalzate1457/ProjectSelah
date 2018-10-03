using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectSelah.API
{
    public class Notifyable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public static event PropertyChangedEventHandler PropertyChanged1;

        public static void NotifyPropertyChanged(object o, [CallerMemberName] string propName = null) =>
            PropertyChanged1?.Invoke(o, new PropertyChangedEventArgs(propName));
    }
}

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace maui_car_list.ViewModels;

// Properties of ViewModel can be observed from application. When the changed a event is fired
internal class BaseViewModel : INotifyPropertyChanged
{
    string title;   // title of page we are on
    bool isBusy;    // is page busy

    public bool IsBusy
    {
        get => isBusy;
        set
        {
            if (isBusy == value) return;
            isBusy = value;
            OnPropertyChanged();
        }
    }

    public string Title
    {
        get => title;
        set
        {
            if (title == value) return;
            title = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    // gets name of property which triggered this and pass it to name attribute
    public void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

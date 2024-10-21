using CommunityToolkit.Mvvm.ComponentModel;

namespace maui_car_list.ViewModels;

// Properties of ViewModel can be observed from application. When the changed a event is fired
// partial cause toolkit is generating code: see Dependencies - net8.0-android-Analyzers-SourceGenerator.ObservablePropertyGenerator
public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    string title;   // title of page we are on

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(isNotBusy))]
    bool isBusy;    // is page busy

    public bool isNotBusy => !isBusy;

}

# Readme

AlsoNotifyChanged -> NotifyPropertyChanged
ICommand -> RelayCommand

Just a little maui testing and learning project :)

## Remarks

### Emulator Errors

1. restart Emulator
1. Delete / Create New Emulator
1. `ADB-Error` ...First RELEASE build then DEBUG build
1. Restart machine
1. clean sln / remove obj/bin folder / build solution
1. DO ALL of the above

## MVVM

### View Model

- Implements **properties** and **commands**, the view can **bind** to
- **Notifies** the view of state changes through **change notification events**
- Properties and commands in viewmodel define fucntionality offered by UI
- **Asynchronous** methods for I/O operations to ensure UI is **unblocked** 

### Implementation
- Class implementing **INotifyPropertyChanged**
- Raise **PropertyChanged** event each time a bound property changes
- Set ViewModel as **BindingContext** in the construcore
- **Bind** UI Elements to properties and commands

Use .NET Community MVVM Toolkit as Framework for DI, components, UI platform integration (others: ReactiveUI, PrismLibrary)
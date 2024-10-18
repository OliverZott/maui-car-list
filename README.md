# Readme

AlsoNotifyChanged -> NotifyPropertyChanged
ICommand -> RelayCommand

Just a little maui testing and learning project :)

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
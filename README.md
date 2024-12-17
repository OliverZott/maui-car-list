# Readme

Example project to learn MAUI ([Course](https://www.udemy.com/course/net-maui-mobile-app-development))

AlsoNotifyChanged -> NotifyPropertyChanged
ICommand -> RelayCommand


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

## UI / UX

- Update inside the Detail View
- Delete Button less present?
- ClearForm less present compared to Add

## ToDo

- Sort by date


## API

### Migration / DB update

- `add-migration <commit_message>`
- `update-database`

If terminal usage (linux/mac):
- `dotnet tool install --global dotnet-ef`
- `dotnet ef migration add <commit_message>`
- `dotnet ef database update`

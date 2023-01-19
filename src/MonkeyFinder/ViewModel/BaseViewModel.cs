using CommunityToolkit.Mvvm.ComponentModel;

namespace MonkeyFinder.ViewModel;

public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;

    [ObservableProperty]
    private string title;

    public bool IsNotBusy => !IsBusy;
}

//public abstract class BaseViewModel : INotifyPropertyChanged
//{
//    private bool isBusy;
//    private string title;

//    public bool IsBusy
//    {
//        get => isBusy;
//        set
//        {
//            if (isBusy == value) return;

//            isBusy = value;

//            OnPropertyChanged();
//            OnPropertyChanged(nameof(IsNotBusy));
//        }
//    }

//    public bool IsNotBusy => !IsBusy;

//    public string Title
//    {
//        get => title;
//        set
//        {
//            if (title == value) return;

//            title = value;

//            OnPropertyChanged();
//        }
//    }

//    public event PropertyChangedEventHandler PropertyChanged;

//    private void OnPropertyChanged([CallerMemberName]string propertyName = null)
//    {
//        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//    }
//}

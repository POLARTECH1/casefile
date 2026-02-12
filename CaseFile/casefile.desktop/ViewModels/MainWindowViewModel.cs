using Microsoft.Extensions.Logging;

namespace casefile.desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    

    public MainWindowViewModel()
    {
    }

    public string Greeting { get; } = "Welcome to Avalonia!";
}
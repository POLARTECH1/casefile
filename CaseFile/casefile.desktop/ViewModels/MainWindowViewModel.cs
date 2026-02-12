using Microsoft.Extensions.Logging;

namespace casefile.desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    
    private readonly ILogger<MainWindowViewModel> _logger;

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger)
    {
        _logger = logger;
    }

    public string Greeting { get; } = "Welcome to Avalonia!";
}
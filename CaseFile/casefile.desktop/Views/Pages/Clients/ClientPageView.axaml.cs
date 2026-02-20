using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using casefile.desktop.ViewModels;
using ReactiveUI;
using ReactiveUI.Avalonia;

namespace casefile.desktop.Views.Pages.Clients;

public partial class ClientPageView : ReactiveUserControl<ClientPageViewModel>
{
    public ClientPageView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}
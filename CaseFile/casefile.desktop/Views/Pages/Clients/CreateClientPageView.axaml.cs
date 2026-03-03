using Avalonia.Markup.Xaml;
using casefile.desktop.ViewModels.Clients;
using ReactiveUI;
using ReactiveUI.Avalonia;

namespace casefile.desktop.Views.Pages.Clients;

public partial class CreateClientPageView : ReactiveUserControl<CreateClientPageViewModel>
{
    public CreateClientPageView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}

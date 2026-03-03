using Avalonia.Markup.Xaml;
using casefile.desktop.ViewModels.Clients;
using ReactiveUI.Avalonia;

namespace casefile.desktop.Views.Pages.Clients;

public partial class ShowClientPageView : ReactiveUserControl<ShowClientPageViewModel>
{
    public ShowClientPageView()
    {
        AvaloniaXamlLoader.Load(this);
    }
}

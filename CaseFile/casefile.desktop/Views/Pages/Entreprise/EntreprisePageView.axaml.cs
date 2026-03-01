using Avalonia.Markup.Xaml;
using casefile.desktop.ViewModels;
using ReactiveUI;
using ReactiveUI.Avalonia;

namespace casefile.desktop.Views.Pages.Entreprise;

public partial class EntreprisePageView : ReactiveUserControl<EntreprisePageViewModel>
{
    public EntreprisePageView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}

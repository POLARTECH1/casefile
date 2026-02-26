using Avalonia.Markup.Xaml;
using casefile.desktop.ViewModels.Schema;
using ReactiveUI;
using ReactiveUI.Avalonia;

namespace casefile.desktop.Views.Pages.Schema;

public partial class SchemaPageView : ReactiveUserControl<SchemaPageViewModel>
{
    public SchemaPageView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using casefile.desktop.Models;
using ReactiveUI;
using ReactiveUI.Avalonia;

namespace casefile.desktop.Views.Pages.Templates;

public partial class TemplatePageView : ReactiveUserControl<TemplatePageViewModel>
{
    public TemplatePageView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}
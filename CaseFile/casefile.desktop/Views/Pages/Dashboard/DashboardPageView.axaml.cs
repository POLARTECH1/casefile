using Avalonia.Markup.Xaml;
using casefile.desktop.ViewModels;
using ReactiveUI;
using ReactiveUI.Avalonia;

namespace casefile.desktop.Views.Pages.Dashboard;

public partial class DashboardPageView : ReactiveUserControl<DashboardPageViewModel>
{
    public DashboardPageView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}

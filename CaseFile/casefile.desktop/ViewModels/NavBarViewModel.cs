using System.Reactive;
using casefile.desktop.Models;
using ReactiveUI;

namespace casefile.desktop.ViewModels;

public class NavBarViewModel
{
    private readonly MainWindowViewModel _shell;

    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToClientPage { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToTemplatePage { get; }

    public NavBarViewModel(MainWindowViewModel shell)
    {
        _shell = shell;

        NavigateToClientPage =
            ReactiveCommand.CreateFromObservable(() =>
                _shell.Router.Navigate.Execute(new ClientPageViewModel(_shell)));

        NavigateToTemplatePage =
            ReactiveCommand.CreateFromObservable(() =>
                _shell.Router.Navigate.Execute(new TemplatePageViewModel(_shell)));
    }
}
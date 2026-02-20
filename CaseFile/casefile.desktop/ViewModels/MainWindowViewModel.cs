using System;
using System.Reactive;
using casefile.desktop.Navigation;
using ReactiveUI;

namespace casefile.desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IScreen
{
    private readonly AppScreen _screen;
    private readonly IAppRouter _appRouter;

    public MainWindowViewModel(AppScreen screen, NavBarViewModel navBar, IAppRouter appRouter)
    {
        _screen = screen;
        _appRouter = appRouter;
        NavBar = navBar;
        GoBack = ReactiveCommand.CreateFromObservable(() => _appRouter.GoBack());

        _appRouter.NavigateTo(AppRoute.Dashboard).Subscribe(new NoOpObserver());
    }

    /// <summary>
    /// Représente la propriété de gestion des états de navigation pour l'application.
    /// Permet la redirection entre les différentes vues dans l'architecture MVVM.
    /// Cette propriété expose un <see cref="RoutingState"/> utilisé pour contrôler
    /// les transitions et maintenir l'état de navigation.
    /// </summary>
    public RoutingState Router => _screen.Router;

    public NavBarViewModel NavBar { get; }

    /// <summary>
    /// Représente une commande permettant de revenir à la vue précédente
    /// dans le cadre de la navigation basée sur le modèle MVVM.
    /// Cette propriété utilise un <see cref="ReactiveCommand{TInput, TResult}"/> pour
    /// déclencher l'action de navigation en arrière via le <see cref="RoutingState"/>
    /// associé.
    /// </summary>
    public ReactiveCommand<Unit, IRoutableViewModel> GoBack { get; }

    private sealed class NoOpObserver : IObserver<IRoutableViewModel>
    {
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(IRoutableViewModel value)
        {
        }
    }
}

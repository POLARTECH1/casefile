using System;
using System.Reactive;
using casefile.desktop.Models;
using Microsoft.Extensions.Logging;
using ReactiveUI;

namespace casefile.desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IScreen
{
    public MainWindowViewModel(IServiceProvider services)
    {
        NavBar = new NavBarViewModel(this);
        /*NavigateToClientPage =
            ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new ClientPageViewModel(this)));
        NavigateToTemplatePage =
            ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new TemplatePageViewModel(this)));*/
    }

    /// <summary>
    /// Représente la propriété de gestion des états de navigation pour l'application.
    /// Permet la redirection entre les différentes vues dans l'architecture MVVM.
    /// Cette propriété expose un <see cref="RoutingState"/> utilisé pour contrôler
    /// les transitions et maintenir l'état de navigation.
    /// </summary>
    public RoutingState Router { get; } = new RoutingState();

    public NavBarViewModel NavBar { get; }
    /* public ReactiveCommand<Unit, IRoutableViewModel> NavigateToClientPage { get; }

     public ReactiveCommand<Unit, IRoutableViewModel> NavigateToTemplatePage { get; }*/

    /// <summary>
    /// Représente une commande permettant de revenir à la vue précédente
    /// dans le cadre de la navigation basée sur le modèle MVVM.
    /// Cette propriété utilise un <see cref="ReactiveCommand{TInput, TResult}"/> pour
    /// déclencher l'action de navigation en arrière via le <see cref="RoutingState"/>
    /// associé.
    /// </summary>
    public ReactiveCommand<Unit, IRoutableViewModel> GoBack => Router.NavigateBack;
}
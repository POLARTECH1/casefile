using System;
using System.Collections.ObjectModel;

namespace casefile.desktop.ViewModels.Clients.SubPages.PageDossier;

public sealed class ShowClientDossiersSubPageViewModel : ViewModelBase
{
    public ShowClientDossiersSubPageViewModel(Guid clientId)
    {
        ClientId = clientId;
    }

    public Guid ClientId { get; }

    /// <summary>
    /// L'ensemble des dossiers virtuels associés au client.
    /// </summary>
    public ObservableCollection<ShowClientDossierSubPageItemViewModel> Dossiers { get; } =
        new ObservableCollection<ShowClientDossierSubPageItemViewModel>();
}
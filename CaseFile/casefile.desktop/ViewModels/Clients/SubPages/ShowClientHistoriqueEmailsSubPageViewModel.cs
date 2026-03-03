using System;

namespace casefile.desktop.ViewModels.Clients.SubPages;

public sealed class ShowClientHistoriqueEmailsSubPageViewModel : ViewModelBase
{
    public ShowClientHistoriqueEmailsSubPageViewModel(Guid clientId)
    {
        ClientId = clientId;
    }

    public Guid ClientId { get; }
}

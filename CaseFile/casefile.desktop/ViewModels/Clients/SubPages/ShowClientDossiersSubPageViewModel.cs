using System;

namespace casefile.desktop.ViewModels.Clients.SubPages;

public sealed class ShowClientDossiersSubPageViewModel : ViewModelBase
{
    public ShowClientDossiersSubPageViewModel(Guid clientId)
    {
        ClientId = clientId;
    }

    public Guid ClientId { get; }
}

using System;

namespace casefile.desktop.ViewModels.Clients.SubPages;

public sealed class ShowClientDocumentsSubPageViewModel : ViewModelBase
{
    public ShowClientDocumentsSubPageViewModel(Guid clientId)
    {
        ClientId = clientId;
    }

    public Guid ClientId { get; }
}

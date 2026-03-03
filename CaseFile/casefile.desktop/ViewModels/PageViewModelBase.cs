using System;
using CommunityToolkit.Mvvm.ComponentModel;
using ReactiveUI;

namespace casefile.desktop.ViewModels;

public abstract class PageViewModelBase : ReactiveObject , IRoutableViewModel
{
    /// <summary>
    /// Reference to IScreen that owns the routable view model.
    /// </summary>
    public IScreen HostScreen { get; }

    /// <summary>
    ///    Unique identifier for the routable view model.
    /// </summary>
    public string UrlPathSegment { get; init; } = Guid.NewGuid().ToString().Substring(0, 5);

    protected PageViewModelBase(IScreen screen) => HostScreen = screen;
}
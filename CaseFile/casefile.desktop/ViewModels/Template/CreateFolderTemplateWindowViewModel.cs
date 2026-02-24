using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using casefile.domain.model;
using casefile.desktop.ViewModels;

namespace casefile.desktop.ViewModels.Template;

public partial class CreateFolderTemplateWindowViewModel : ViewModelBase
{
    [ObservableProperty] private string _nom = string.Empty;
    [ObservableProperty] private string? _description;
}
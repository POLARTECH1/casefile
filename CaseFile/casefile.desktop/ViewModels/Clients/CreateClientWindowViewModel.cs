using System;
using System.Collections.Generic;
using casefile.domain.model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace casefile.desktop.ViewModels.Clients;

/// <summary>
/// ViewModel pour la fenêtre de création d'un client.
/// </summary>
public partial class CreateClientWindowViewModel : ViewModelBase
{
    [ObservableProperty] private string _nom = string.Empty;
    [ObservableProperty] private string _prenom = string.Empty;
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _telephone = string.Empty;
    [ObservableProperty] private Guid? _selectedTemplateId;
    [ObservableProperty] private Guid? _selectedSchemaId;

    public List<CreateClientFormulaireDefinitionAttributElement> ChampsDAttributs { get; set; } =
        new List<CreateClientFormulaireDefinitionAttributElement>();
}
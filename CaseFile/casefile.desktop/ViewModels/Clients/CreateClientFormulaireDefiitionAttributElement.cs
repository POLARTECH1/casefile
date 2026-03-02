using System;
using casefile.domain.model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace casefile.desktop.ViewModels.Clients;

/// <summary>
/// ViewModel représentant un élément de champ d'attribut dans le formulaire de création d'un client.
/// </summary>
public partial class CreateClientFormulaireDefinitionAttributElement : ObservableObject
{
    /// <summary>
    /// Identifiant unique de la définition.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Libellé affiché de l'attribut.
    /// </summary>
    public string Libelle { get; set; } = string.Empty;

    /// <summary>
    /// Type de valeur attendu.
    /// </summary>
    public TypeAttribut Type { get; set; }

    /// <summary>
    /// Valeur par défaut éventuelle.
    /// </summary>
    public string? ValeurDefaut { get; set; }

    /// <summary>
    /// Indique si l'attribut est obligatoire.
    /// </summary>
    public bool EstRequis { get; set; }
    [ObservableProperty] private string _stringValue = string.Empty;
    [ObservableProperty] private int _numberValue = 0;
    [ObservableProperty] private DateTime _dateValue = DateTime.Now;

    public bool IsChampTexte => Type == TypeAttribut.Texte;
    public bool IsChampNombre => Type == TypeAttribut.Nombre;
    public bool IsChampDate => Type == TypeAttribut.Date;
}
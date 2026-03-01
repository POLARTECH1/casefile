using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using casefile.domain.model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace casefile.desktop.ViewModels.Schema;

public partial class SchemaPropertyItemViewModel : ObservableObject
{
    private readonly ObservableCollection<SchemaPropertyItemViewModel> _parentCollection;

    public IReadOnlyList<TypeAttribut> TypesAttribut { get; } = Enum.GetValues<TypeAttribut>();

    public SchemaPropertyItemViewModel(ObservableCollection<SchemaPropertyItemViewModel> parentCollection)
    {
        _parentCollection = parentCollection;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasLibelleError))]
    private string _libelle = string.Empty;

    [ObservableProperty] private TypeAttribut _type = TypeAttribut.Texte;
    [ObservableProperty] private bool _estRequis;
    [ObservableProperty] private string? _valeurDefaut;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasLibelleError))]
    private string? _libelleError;

    public bool HasLibelleError => LibelleError != null;

    public void ValidateLibelle()
    {
        if (string.IsNullOrWhiteSpace(Libelle))
            LibelleError = "Le nom de la propriété est obligatoire.";
        else if (Libelle.Trim().Length > 200)
            LibelleError = "Le nom de la propriété ne peut pas dépasser 200 caractères.";
        else
            LibelleError = null;
    }

    partial void OnLibelleChanged(string value)
    {
        if (LibelleError != null)
        {
            ValidateLibelle();
        }
    }

    [RelayCommand]
    private void Remove() => _parentCollection.Remove(this);
}

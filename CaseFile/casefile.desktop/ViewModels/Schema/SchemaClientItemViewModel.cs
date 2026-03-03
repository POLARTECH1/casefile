using System;
using CommunityToolkit.Mvvm.Input;

namespace casefile.desktop.ViewModels.Schema;

public class SchemaClientItemViewModel
{
    public required Guid Id { get; set; }
    public required string Nom { get; set; } = string.Empty;
    public required string DescriptionCourte { get; set; } = string.Empty;
    public required int NombreDeProprietes { get; set; }
    public required string NombreDeClientsQuiUtilisentCeSchema { get; set; } = string.Empty;

    public IAsyncRelayCommand? SupprimerCommand { get; set; }
    public IAsyncRelayCommand? ModifierCommand { get; set; }
    public IAsyncRelayCommand? OuvrirCommand { get; set; }
}

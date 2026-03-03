using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace casefile.desktop.ViewModels.Clients;

/// <summary>
/// Les éléments de la liste des clients sont représentés par cette classe. Elle contient les propriétés nécessaires pour afficher les informations de base d'un client dans la liste, telles que son nom, son prénom, et éventuellement d'autres détails pertinents.
/// </summary>
public partial class ClientListItemViewModel : ObservableObject
{
    /// <summary>
    /// Identifiant unique du client.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Nom complet du client, combinant le prénom et le nom de famille pour une présentation plus conviviale dans la liste.
    /// </summary>
    public required string NomPrenom { get; set; }

    /// <summary>
    /// Adresse courriel du client.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Nom du Schéma d'attributs utilisé pour ce client.
    /// </summary>
    public required string NomSchema { get; set; }

    /// <summary>
    /// Nombre total de documents associés au client.
    /// </summary>
    public required int NombreDocuments { get; set; }

    /// <summary>
    /// Indique si l'élément de la liste des clients est actuellement sélectionné.
    /// </summary>
    [ObservableProperty] private bool _isSelected;

    /// <summary>
    /// Commande permettant d'ouvrir les détails d'un client spécifique.
    /// </summary>
    public IAsyncRelayCommand? OuvrirClientCommand { get; set; }

    /// <summary>
    /// Commande asynchrone utilisée pour supprimer un client de la liste.
    /// </summary>
    public IAsyncRelayCommand? SupprimerClientCommand { get; set; }

    /// <summary>
    ///  Commandes asynchrone utilisée pour ouvrir la fenêtre de modification d'un client spécifique.
    /// </summary>
    public IAsyncRelayCommand? ModifierClientCommand { get; set; }

    /// <summary>
    /// Commande asynchrone permettant d'exporter le dossier d'un client.
    /// </summary>
    public IAsyncRelayCommand? ExporterDossierClientCommand { get; set; }
}
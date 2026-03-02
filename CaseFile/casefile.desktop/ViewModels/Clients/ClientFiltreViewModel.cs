using System.Collections.Generic;
using casefile.application.DTOs.Client;
using CommunityToolkit.Mvvm.ComponentModel;

namespace casefile.desktop.ViewModels.Clients;

/// <summary>
/// Encapsule l'état des filtres de la page clients.
/// Peut être réinitialisé ou sérialisé indépendamment du ViewModel principal.
/// </summary>
public partial class ClientFiltreViewModel : ObservableObject
{
    // --- Recherche texte libre ---
    [ObservableProperty] private string _rechercheTexte = string.Empty;

    // --- Filtres par schéma ---
    [ObservableProperty] private bool _filtreParticulier;
    [ObservableProperty] private bool _filtreEntreprise;
    [ObservableProperty] private bool _filtreVip;

    // --- Filtres par nombre de documents ---
    [ObservableProperty] private bool _filtreDocuments0a10;
    [ObservableProperty] private bool _filtreDocuments11a20;
    [ObservableProperty] private bool _filtreDocuments20Plus;
    [ObservableProperty] private bool _filtreDocumentsTous = true;

    /// <summary>
    /// Construit le DTO de filtre à passer à IGetClientItems.
    /// NomSchema : si un seul schéma est coché, on filtre dessus (Contains côté spec).
    /// NombreDocuments : non utilisé ici — les plages sont appliquées côté client via CorrespondAuFiltreDocuments.
    /// </summary>
    public ClientItemFilterDto VersDto()
    {
        var schemas = new List<string>();
        if (FiltreParticulier) schemas.Add("Particulier");
        if (FiltreEntreprise) schemas.Add("Entreprise");
        if (FiltreVip) schemas.Add("VIP");

        return new ClientItemFilterDto
        {
            NomPrenom = string.IsNullOrWhiteSpace(RechercheTexte) ? null : RechercheTexte.Trim(),
            NomSchema = schemas.Count == 1 ? schemas[0] : null
        };
    }

    /// <summary>
    /// Vérifie si un nombre de documents correspond aux plages sélectionnées.
    /// Appliqué côté client après la requête, car la spec ne supporte pas les plages.
    /// </summary>
    public bool CorrespondAuFiltreDocuments(int nombreDocuments)
    {
        if (FiltreDocumentsTous) return true;
        if (!FiltreDocuments0a10 && !FiltreDocuments11a20 && !FiltreDocuments20Plus) return true;

        if (FiltreDocuments0a10 && nombreDocuments <= 10) return true;
        if (FiltreDocuments11a20 && nombreDocuments is >= 11 and <= 20) return true;
        if (FiltreDocuments20Plus && nombreDocuments > 20) return true;

        return false;
    }
}

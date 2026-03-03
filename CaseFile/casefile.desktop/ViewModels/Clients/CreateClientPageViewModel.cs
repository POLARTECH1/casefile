using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using casefile.application.DTOs.Client;
using casefile.application.DTOs.ValeurAttributClient;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.Navigation;
using casefile.desktop.ViewModels;
using casefile.domain.model;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace casefile.desktop.ViewModels.Clients;

/// <summary>
/// ViewModel pour la page de création d'un nouveau client.
/// Gère le formulaire, les dropdowns paginés de schéma et template,
/// les champs dynamiques et la validation.
/// </summary>
public partial class CreateClientPageViewModel : PageViewModelBase
{
    private const int PageSize = 10;

    private readonly IGetSchemaClientsForSelect _getSchemaClientsForSelect;
    private readonly IGetTemplateDossiersForSelect _getTemplateDossiersForSelect;
    private readonly ICreateClient _createClient;
    private readonly IAppRouter _router;


    private string _nom = string.Empty;

    public string Nom
    {
        get => _nom;
        set => this.RaiseAndSetIfChanged(ref _nom, value);
    }

    private string _prenom = string.Empty;

    public string Prenom
    {
        get => _prenom;
        set => this.RaiseAndSetIfChanged(ref _prenom, value);
    }

    private string _email = string.Empty;

    public string Email
    {
        get => _email;
        set => this.RaiseAndSetIfChanged(ref _email, value);
    }

    private string _telephone = string.Empty;

    public string Telephone
    {
        get => _telephone;
        set => this.RaiseAndSetIfChanged(ref _telephone, value);
    }


    private CancellationTokenSource? _schemaSearchCts;
    private CancellationTokenSource? _templateSearchCts;

    private string _rechercheSchema = string.Empty;

    public string RechercheSchema
    {
        get => _rechercheSchema;
        set
        {
            this.RaiseAndSetIfChanged(ref _rechercheSchema, value);
            _ = DebounceSchemaSearchAsync();
        }
    }

    private int _pageSchema = 1;

    public int PageSchema
    {
        get => _pageSchema;
        set
        {
            this.RaiseAndSetIfChanged(ref _pageSchema, value);
            this.RaisePropertyChanged(nameof(PaginationSchemaLabel));
        }
    }

    private int _totalPagesSchema = 1;

    public int TotalPagesSchema
    {
        get => _totalPagesSchema;
        set
        {
            this.RaiseAndSetIfChanged(ref _totalPagesSchema, value);
            this.RaisePropertyChanged(nameof(PaginationSchemaLabel));
        }
    }

    private SchemaClientDropdownItemViewModel? _schemaSelectionne;

    public SchemaClientDropdownItemViewModel? SchemaSelectionne
    {
        get => _schemaSelectionne;
        set
        {
            this.RaiseAndSetIfChanged(ref _schemaSelectionne, value);
            this.RaisePropertyChanged(nameof(SchemaSelectionneNom));
            MettreAJourChampsDAttributs();
        }
    }

    public string SchemaSelectionneNom => _schemaSelectionne?.Nom ?? "Sélectionner un schéma...";
    public string PaginationSchemaLabel => $"Page {PageSchema} / {TotalPagesSchema}";

    public ObservableCollection<SchemaClientDropdownItemViewModel> ListeSchemas { get; } = new();


    private string _rechercheTemplate = string.Empty;

    public string RechercheTemplate
    {
        get => _rechercheTemplate;
        set
        {
            this.RaiseAndSetIfChanged(ref _rechercheTemplate, value);
            _ = DebounceTemplateSearchAsync();
        }
    }

    private int _pageTemplate = 1;

    public int PageTemplate
    {
        get => _pageTemplate;
        set
        {
            this.RaiseAndSetIfChanged(ref _pageTemplate, value);
            this.RaisePropertyChanged(nameof(PaginationTemplateLabel));
        }
    }

    private int _totalPagesTemplate = 1;

    public int TotalPagesTemplate
    {
        get => _totalPagesTemplate;
        set
        {
            this.RaiseAndSetIfChanged(ref _totalPagesTemplate, value);
            this.RaisePropertyChanged(nameof(PaginationTemplateLabel));
        }
    }

    private TemplateDossierDropdownItemViewModel? _templateSelectionne;

    public TemplateDossierDropdownItemViewModel? TemplateSelectionne
    {
        get => _templateSelectionne;
        set
        {
            this.RaiseAndSetIfChanged(ref _templateSelectionne, value);
            this.RaisePropertyChanged(nameof(TemplateSelectionneNom));
        }
    }

    public string TemplateSelectionneNom => _templateSelectionne?.Nom ?? "Sélectionner un template...";
    public string PaginationTemplateLabel => $"Page {PageTemplate} / {TotalPagesTemplate}";

    public ObservableCollection<TemplateDossierDropdownItemViewModel> ListeTemplates { get; } = new();


    public ObservableCollection<CreateClientFormulaireDefinitionAttributElement> ChampsDAttributs { get; } = new();

    private bool _aDesChampsDAttributs;

    public bool ADesChampsDAttributs
    {
        get => _aDesChampsDAttributs;
        set => this.RaiseAndSetIfChanged(ref _aDesChampsDAttributs, value);
    }


    #region Propriétés de validation et d'erreurs

    public ObservableCollection<string> Erreurs { get; } = new();

    private bool _aDesErreurs;

    public bool ADesErreurs
    {
        get => _aDesErreurs;
        set => this.RaiseAndSetIfChanged(ref _aDesErreurs, value);
    }

    #endregion


    public CreateClientPageViewModel(
        IScreen screen,
        IGetSchemaClientsForSelect getSchemaClientsForSelect,
        IGetTemplateDossiersForSelect getTemplateDossiersForSelect,
        ICreateClient createClient,
        IAppRouter router) : base(screen)
    {
        _getSchemaClientsForSelect = getSchemaClientsForSelect;
        _getTemplateDossiersForSelect = getTemplateDossiersForSelect;
        _createClient = createClient;
        _router = router;

        _ = ChargerSchemasAsync();
        _ = ChargerTemplatesAsync();
    }


    #region Debounce des recherches

    /// <summary>
    /// Gère une recherche différée pour les schémas en introduisant un délai
    /// avant l'exécution de la recherche. Cela permet d'éviter des appels fréquents
    /// pendant que l'utilisateur saisit ou modifie les critères de recherche.
    /// </summary>
    /// <returns>
    /// Une tâche asynchrone représentant l'opération de gestion du délai et du déclenchement de la recherche
    /// des schémas.
    /// </returns>
    private async Task DebounceSchemaSearchAsync()
    {
        _schemaSearchCts?.Cancel();
        _schemaSearchCts = new CancellationTokenSource();
        var cts = _schemaSearchCts;
        try
        {
            await Task.Delay(300, cts.Token);
            _pageSchema = 1;
            this.RaisePropertyChanged(nameof(PageSchema));
            this.RaisePropertyChanged(nameof(PaginationSchemaLabel));
            await ChargerSchemasAsync();
        }
        catch (TaskCanceledException)
        {
        }
    }

    /// <summary>
    /// Gère une recherche différée pour les templates en introduisant un délai
    /// avant l'exécution de la recherche. Cela permet de limiter les appels fréquents
    /// lors de la modification des critères de recherche par l'utilisateur.
    /// </summary>
    /// <returns>
    /// Une tâche asynchrone qui représente l'opération de gestion du délai et de déclenchement
    /// de la recherche des templates.
    /// </returns>
    private async Task DebounceTemplateSearchAsync()
    {
        _templateSearchCts?.Cancel();
        _templateSearchCts = new CancellationTokenSource();
        var cts = _templateSearchCts;
        try
        {
            await Task.Delay(300, cts.Token);
            _pageTemplate = 1;
            this.RaisePropertyChanged(nameof(PageTemplate));
            this.RaisePropertyChanged(nameof(PaginationTemplateLabel));
            await ChargerTemplatesAsync();
        }
        catch (TaskCanceledException)
        {
        }
    }

    #endregion

    #region Commandes de navigation

    [RelayCommand]
    private void RetournerVersListe() => _router.NavigateTo(AppRoute.Clients).Subscribe();

    #endregion

    #region Commandes pagination schéma

    [RelayCommand]
    private async Task PageSchemaAvant()
    {
        if (PageSchema >= TotalPagesSchema) return;
        PageSchema++;
        await ChargerSchemasAsync();
    }

    [RelayCommand]
    private async Task PageSchemaArriere()
    {
        if (PageSchema <= 1) return;
        PageSchema--;
        await ChargerSchemasAsync();
    }

    [RelayCommand]
    private void SelectionnerSchema(SchemaClientDropdownItemViewModel schema)
    {
        SchemaSelectionne = schema;
    }

    #endregion

    #region Commandes pagination template

    [RelayCommand]
    private async Task PageTemplateAvant()
    {
        if (PageTemplate >= TotalPagesTemplate) return;
        PageTemplate++;
        await ChargerTemplatesAsync();
    }

    [RelayCommand]
    private async Task PageTemplateArriere()
    {
        if (PageTemplate <= 1) return;
        PageTemplate--;
        await ChargerTemplatesAsync();
    }

    [RelayCommand]
    private void SelectionnerTemplate(TemplateDossierDropdownItemViewModel template)
    {
        TemplateSelectionne = template;
    }

    #endregion


    #region Commande de création

    [RelayCommand]
    private async Task CreerClient()
    {
        var erreurs = Valider();
        Erreurs.Clear();
        foreach (var e in erreurs) Erreurs.Add(e);
        ADesErreurs = erreurs.Count > 0;
        if (ADesErreurs) return;

        var dto = new CreateClientDto
        {
            Nom = Nom.Trim(),
            Prenom = Prenom.Trim(),
            Email = Email.Trim(),
            Telephone = Telephone.Trim(),
            SchemaClientId = SchemaSelectionne?.Id,
            TemplateDossierId = TemplateSelectionne?.Id,
            ValeursAttributs = ChampsDAttributs.Select(champ => new CreateValeurAttributClientDto
            {
                Cle = champ.Cle,
                Valeur = ObtenirValeurChamp(champ)
            }).ToList()
        };

        var result = await _createClient.ExecuteAsync(dto);
        if (result.IsFailed)
        {
            Erreurs.Clear();
            Erreurs.Add("Une erreur est survenue lors de la création du client. Veuillez réessayer.");
            ADesErreurs = true;
            return;
        }

        _router.NavigateTo(AppRoute.Clients).Subscribe();
    }

    #endregion


    private List<string> Valider()
    {
        var erreurs = new List<string>();

        if (string.IsNullOrWhiteSpace(Nom))
            erreurs.Add("Le nom du client est obligatoire.");
        else if (Nom.Trim().Length > 150)
            erreurs.Add("Le nom ne peut pas dépasser 150 caractères.");

        if (string.IsNullOrWhiteSpace(Prenom))
            erreurs.Add("Le prénom du client est obligatoire.");
        else if (Prenom.Trim().Length > 150)
            erreurs.Add("Le prénom ne peut pas dépasser 150 caractères.");

        if (string.IsNullOrWhiteSpace(Email))
            erreurs.Add("L'email du client est obligatoire.");
        else if (!EstEmailValide(Email.Trim()))
            erreurs.Add("L'email du client n'est pas valide.");

        if (string.IsNullOrWhiteSpace(Telephone))
            erreurs.Add("Le téléphone du client est obligatoire.");

        foreach (var champ in ChampsDAttributs)
        {
            if (!champ.EstRequis) continue;

            if (champ.IsChampTexte && string.IsNullOrWhiteSpace(champ.StringValue))
                erreurs.Add($"Le champ « {champ.Libelle} » est obligatoire.");
            else if (champ.IsChampNombre && champ.NumberValue == null)
                erreurs.Add($"Le champ « {champ.Libelle} » est obligatoire.");
            else if (champ.IsChampDate && champ.DateValue == null)
                erreurs.Add($"Le champ « {champ.Libelle} » est obligatoire.");
        }

        return erreurs;
    }

    /// <summary>
    /// Charge et met à jour la liste des schémas disponibles en fonction des critères de recherche
    /// et de pagination actuels.
    /// </summary>
    /// <returns>
    /// Une tâche asynchrone représentant l'opération de chargement des schémas,
    /// mettant à jour la collection de schémas ainsi que le nombre total de pages.
    /// </returns>
    private async Task ChargerSchemasAsync()
    {
        var result = await _getSchemaClientsForSelect.ExecuteAsync(RechercheSchema, PageSchema, PageSize);
        if (result.IsFailed) return;

        var (items, totalCount) = result.Value;

        ListeSchemas.Clear();
        foreach (var schema in items)
        {
            var vm = new SchemaClientDropdownItemViewModel
            {
                Id = schema.Id,
                Nom = schema.Nom,
                Definitions = schema.Definitions
            };
            vm.SelectionnerCommand = new RelayCommand(() => SelectionnerSchema(vm));
            ListeSchemas.Add(vm);
        }

        TotalPagesSchema = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));
    }

    /// <summary>
    /// Charge et met à jour la liste des templates disponibles en fonction des critères de recherche et de pagination.
    /// </summary>
    /// <returns>
    /// Une tâche asynchrone représentant l'opération de chargement des templates,
    /// qui met à jour la collection des templates et le nombre total de pages.
    /// </returns>
    private async Task ChargerTemplatesAsync()
    {
        var result = await _getTemplateDossiersForSelect.ExecuteAsync(RechercheTemplate, PageTemplate, PageSize);
        if (result.IsFailed) return;

        var (items, totalCount) = result.Value;

        ListeTemplates.Clear();
        foreach (var template in items)
        {
            var vm = new TemplateDossierDropdownItemViewModel
            {
                Id = template.Id,
                Nom = template.Nom
            };
            vm.SelectionnerCommand = new RelayCommand(() => SelectionnerTemplate(vm));
            ListeTemplates.Add(vm);
        }

        TotalPagesTemplate = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));
    }

    /// <summary>
    /// Met à jour la liste des champs d'attributs en fonction du schéma sélectionné.
    /// </summary>
    private void MettreAJourChampsDAttributs()
    {
        ChampsDAttributs.Clear();

        if (SchemaSelectionne == null)
        {
            ADesChampsDAttributs = false;
            return;
        }

        foreach (var def in SchemaSelectionne.Definitions)
        {
            var champ = new CreateClientFormulaireDefinitionAttributElement
            {
                Id = def.Id,
                Cle = def.Cle,
                Libelle = def.Libelle,
                Type = def.Type,
                EstRequis = def.EstRequis,
                ValeurDefaut = def.ValeurDefaut
            };

            // Appliquer la valeur par défaut si disponible
            if (!string.IsNullOrEmpty(def.ValeurDefaut))
            {
                if (champ.IsChampTexte)
                    champ.StringValue = def.ValeurDefaut;
                else if (champ.IsChampNombre && decimal.TryParse(def.ValeurDefaut, out var num))
                    champ.NumberValue = num;
                else if (champ.IsChampBooleen && bool.TryParse(def.ValeurDefaut, out var b))
                    champ.BoolValue = b;
            }

            ChampsDAttributs.Add(champ);
        }

        ADesChampsDAttributs = ChampsDAttributs.Count > 0;
    }

    /// <summary>
    /// Obtient la valeur d'un champ spécifique à partir de sa définition d'attribut.
    /// </summary>
    /// <param name="champ">La définition de l'attribut du champ à partir duquel la valeur doit être obtenue.</param>
    /// <returns>Retourne la valeur du champ sous forme de chaîne, formatée en fonction de son type (texte, nombre, date ou booléen).</returns>
    private static string ObtenirValeurChamp(CreateClientFormulaireDefinitionAttributElement champ)
    {
        if (champ.IsChampTexte) return champ.StringValue;
        if (champ.IsChampNombre) return champ.NumberValue?.ToString() ?? "0";
        if (champ.IsChampDate) return champ.DateValue?.ToString("yyyy-MM-dd") ?? string.Empty;
        if (champ.IsChampBooleen) return champ.BoolValue.ToString().ToLower();
        return string.Empty;
    }

    /// <summary>
    /// Vérifie si une adresse email donnée est valide.
    /// </summary>
    /// <param name="email">L'adresse email à valider.</param>
    /// <returns>Retourne true si l'email est valide, sinon false.</returns>
    private static bool EstEmailValide(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
namespace casefile.desktop.Services;

/// <summary>
/// Représente une requête vide utilisée dans le cadre de traitements ne nécessitant pas
/// de paramètres. Cette structure est destinée à être utilisée dans des scénarios où aucun
/// paramètre d'entrée n'est requis pour effectuer une opération ou afficher une fenêtre de dialogue.
/// Exemple courant : ouvrir un dialogue sans données initiales.
/// </summary>
public readonly record struct NoDialogRequest;

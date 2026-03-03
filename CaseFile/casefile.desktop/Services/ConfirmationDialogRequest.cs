using System;

namespace casefile.desktop.Services;

/// <summary>
/// Représente une demande de boîte de dialogue de confirmation, fournissant les informations nécessaires pour afficher une fenêtre
/// de confirmation interactive à l'utilisateur.
/// </summary>
/// <remarks>
/// Cette classe encapsule les paramètres requis pour afficher une fenêtre modale, tels que le message principal, les étiquettes des
/// boutons, un titre optionnel, le résultat de l'interaction utilisateur et une action à exécuter lorsque la boîte de dialogue est fermée.
/// </remarks>
/// <param name="Message">
/// Message principal à afficher dans la boîte de dialogue.
/// </param>
/// <param name="ConfirmButtonText">
/// Texte du bouton de confirmation. Si nul, une valeur par défaut sera utilisée.
/// </param>
/// <param name="CancelButtonText">
/// Texte du bouton d'annulation. Si nul, une valeur par défaut sera utilisée.
/// </param>
/// <param name="Title">
/// Titre de la boîte de dialogue (optionnel).
/// </param>
/// <param name="Result">
/// Résultat de la boîte de dialogue indiquant l'action choisie par l'utilisateur (true, false, ou null si aucune action n'a été sélectionnée).
/// </param>
/// <param name="CloseRequested">
/// Action appelée lorsque l'utilisateur ferme la boîte de dialogue.
/// </param>
public sealed record ConfirmationDialogRequest(
    string Message,
    string? ConfirmButtonText = null,
    string? CancelButtonText = null,
    string? Title = null,
    bool? Result = null,
    Action<bool?>? CloseRequested = null);

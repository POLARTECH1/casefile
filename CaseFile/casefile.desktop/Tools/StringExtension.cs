using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace casefile.desktop.Tools;

public static class StringExtension
{
    /// <summary>
    /// Génère une clé unique basée sur une chaîne d'entrée en normalisant,
    /// en supprimant les caractères non alphabétiques, puis en les remplaçant
    /// par des caractères valides dans une structure prédéfinie.
    /// </summary>
    /// <param name="str">
    /// La chaîne d'entrée à partir de laquelle la clé sera générée.
    /// </param>
    /// <param name="index">
    /// L'index utilisé pour générer un identifiant par défaut si la chaîne
    /// n'est pas valide.
    /// </param>
    /// <param name="maxLength">
    /// La longueur maximale de la chaîne générée.
    /// </param>
    /// <returns>
    /// Une chaîne formatée correspondant à la clé unique générée.
    /// </returns>
    public static string BuildKey(this string str, int index, int maxLength)
    {
        var normalized = str.Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder(normalized.Length);

        foreach (var c in normalized)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(c);
            if (category != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(c);
            }
        }

        var ascii = builder.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant().Trim();
        ascii = Regex.Replace(ascii, "[^a-z0-9]+", "_").Trim('_');

        if (string.IsNullOrWhiteSpace(ascii))
        {
            ascii = $"propriete_{index + 1}";
        }

        return ascii.Length <= 100 ? ascii : ascii[..maxLength];
    }
}
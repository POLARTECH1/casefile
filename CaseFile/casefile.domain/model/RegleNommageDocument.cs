namespace casefile.domain.model;

/// <summary>
/// Règle de nommage associée à un type de document.
/// </summary>
public class RegleNommageDocument
{
    /// <summary>
    /// Identifiant unique de la règle.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Pattern de nommage.
    /// </summary>
    public string Pattern { get; set; } = string.Empty;
}

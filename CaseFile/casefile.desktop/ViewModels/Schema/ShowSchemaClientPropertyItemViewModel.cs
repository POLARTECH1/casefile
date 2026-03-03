namespace casefile.desktop.ViewModels.Schema;

public class ShowSchemaClientPropertyItemViewModel
{
    public string Libelle { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string ValeurDefaut { get; set; } = "-";
    public bool EstRequis { get; set; }
}

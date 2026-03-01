using Avalonia.Controls;
using casefile.desktop.ViewModels.Schema;

namespace casefile.desktop.Views.WindowModal.Schema;

public partial class ShowSchemaClientWindow : Window
{
    public ShowSchemaClientWindow()
    {
        InitializeComponent();
    }

    public ShowSchemaClientWindow(ShowSchemaClientWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}

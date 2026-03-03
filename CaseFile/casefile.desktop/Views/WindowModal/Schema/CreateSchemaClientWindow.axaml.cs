using Avalonia.Controls;
using casefile.desktop.ViewModels.Schema;

namespace casefile.desktop.Views.WindowModal.Schema;

public partial class CreateSchemaClientWindow : Window
{
    public CreateSchemaClientWindow()
    {
        InitializeComponent();
    }

    public CreateSchemaClientWindow(CreateSchemaClientWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        viewModel.CloseRequested += result => Close(result);
    }
}

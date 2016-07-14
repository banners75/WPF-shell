using System.Windows.Controls;
using PopupModule.ViewModels;

namespace PopupModule.Views
{
    /// <summary>
    /// Interaction logic for PopupModuleButton.xaml
    /// </summary>
    public partial class PopupModuleButton : UserControl
    {
        public PopupModuleButton(PopupModuleButtonViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}

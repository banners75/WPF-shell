using System.Windows;
using MahApps.Metro.Controls;
using Shell.ViewModels;

namespace Shell
{
    /// <summary>
    /// Interaction logic for MainShell.xaml
    /// </summary>
    public partial class MainShell : MetroWindow
    {
        public MainShell(MainShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            this.Closed += MainShell_Closed;
        }

        void MainShell_Closed(object sender, System.EventArgs e)
        {
            ((MainShellViewModel)DataContext).Closing();
        }
    }
}

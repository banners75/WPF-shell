using System.Windows.Controls;

namespace Infrastructure.Views
{
    public class ViewControl : UserControl, IView
    {
        public string Title { get; set; }
    }
}

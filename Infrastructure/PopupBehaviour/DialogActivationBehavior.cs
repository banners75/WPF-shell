using System.Collections.Specialized;
using System.Windows;
using Infrastructure.Views;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Regions.Behaviors;

namespace Infrastructure.PopupBehaviour
{
    /// <summary>
    /// Defines a behavior that creates a Dialog to display the active view of the target <see cref="IRegion"/>.
    /// </summary>
    public abstract class DialogActivationBehavior : RegionBehavior, IHostAwareRegionBehavior
    {
        /// <summary>
        /// The key of this behavior
        /// </summary>
        public const string BehaviorKey = "DialogActivation";

        /// <summary>
        /// Gets or sets the <see cref="DependencyObject"/> that the <see cref="IRegion"/> is attached to.
        /// </summary>
        /// <value>A <see cref="DependencyObject"/> that the <see cref="IRegion"/> is attached to.
        /// This is usually a <see cref="FrameworkElement"/> that is part of the tree.</value>
        public DependencyObject HostControl { get; set; }

        /// <summary>
        /// Performs the logic after the behavior has been attached.
        /// </summary>
        protected override void OnAttach()
        {
            Region.Views.CollectionChanged += ActiveViewsCollectionChanged;
        }

        /// <summary>
        /// Override this method to create an instance of the <see cref="IWindow"/> that 
        /// will be shown when a view is activated.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="IWindow"/> that will be shown when a 
        /// view is activated on the target <see cref="IRegion"/>.
        /// </returns>
        protected abstract IWindow CreateWindow(double width, double height, string title);

        private void ActiveViewsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                PrepareContentDialog(e.NewItems[0]);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                CloseContentDialog(((FrameworkElement)e.OldItems[0]).Parent as Window);
            }
        }

        private Style GetStyleForView()
        {
            return HostControl.GetValue(RegionSinglePopupBehaviors.ContainerWindowStyleProperty) as Style;
        }

        private void PrepareContentDialog(object view)
        {
            double width = 100;
            double height = 100;
            var title = string.Empty;

            if (view is IView)
            {
                var configurableView = (IView)view;
                width = configurableView.Width;
                height = configurableView.Height;
                title = string.IsNullOrEmpty(configurableView.Title) ? string.Empty : configurableView.Title;
            }

            var contentDialog = CreateWindow(width, height, title);
            contentDialog.Content = view;
            //contentDialog.Owner = HostControl;
            contentDialog.Closed += ContentDialogClosed;
            contentDialog.Style = GetStyleForView();
            contentDialog.Show();
        }

        private void CloseContentDialog(Window contentDialog)
        {
            if (contentDialog != null)
            {
                contentDialog.Closed -= ContentDialogClosed;
                contentDialog.Close();
                contentDialog.Content = null;
                contentDialog.Owner = null;
            }
        }

        private void ContentDialogClosed(object sender, System.EventArgs e)
        {
            var window = sender as Window;

            if (window != null)
            {
                Region.Deactivate(window.Content);
                CloseContentDialog(window);
            }
        }
    }
}

using System.Windows;
using System.Windows.Controls;

namespace ControlLibrary
{
    [TemplatePart(Name = MainContent, Type = typeof(ContentControl))]
    public class OverlayModalDialog : ContentControl
    {
        public const string MainContent = "PART_Content";

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof (bool), typeof (OverlayModalDialog), new PropertyMetadata(default(bool)));

        private ContentControl _content;

        public bool IsActive
        {
            get { return (bool) GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        static OverlayModalDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OverlayModalDialog), new FrameworkPropertyMetadata(typeof(OverlayModalDialog)));
        }

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (_content != null && IsActive)
            {
                _content.Focus();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _content = GetTemplateChild(MainContent) as ContentControl;
        }
    }
}

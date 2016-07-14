namespace Infrastructure.PopupBehaviour
{
    public class WindowDialogActivationBehavior : DialogActivationBehavior
    {
        protected override IWindow CreateWindow(double width, double height, string title)
        {
            return new WindowWrapper(width, height, title);
        }
    }
}

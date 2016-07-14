namespace Infrastructure.PopupBehaviour
{
    public class WindowModalDialogActivationBehavior : DialogActivationBehavior
    {
        protected override IWindow CreateWindow(double width, double height, string title)
        {
            return new ModalWindowWrapper(width, height, title);
        }
    }
}

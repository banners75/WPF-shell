namespace Infrastructure.PopupBehaviour
{
    public class ModalWindowWrapper : WindowWrapper
    {
        public ModalWindowWrapper(double width, double height, string title) : base(width, height, title)
        {
        }

        public override void Show()
        {
            Window.ShowDialog();
        }
    }
}
namespace Infrastructure.Navigation
{
    public interface IViewService
    {
        void ShowView(object view, string region, double width, double height);
        void CloseAll();
    }
}
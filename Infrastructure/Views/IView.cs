namespace Infrastructure.Views
{
    public interface IView
    {
        double Height { get; set; }
        double Width { get; set; }
        string Title { get; set; }
    }
}

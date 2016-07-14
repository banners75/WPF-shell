using System.Windows.Input;
using Infrastructure;
using Infrastructure.Navigation;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Unity;
using PopupModule.Views;

namespace PopupModule.ViewModels
{
    public class PopupModuleButtonViewModel : NotificationObject
    {
        private readonly IUnityContainer _container;
        private readonly IViewService _viewService;

        public PopupModuleButtonViewModel(IUnityContainer container, IViewService viewService)
        {
            _container = container;
            _viewService = viewService;

            PopupModuleCommand = new DelegateCommand(PopupModule);
        }

        public ICommand PopupModuleCommand { get; set; }

        private void PopupModule()
        {
            var view = _container.Resolve(typeof (MainPage));
            _viewService.ShowView(view, Regions.MultipleModelessPopupRegion, 600, 600);
        }
    }
}

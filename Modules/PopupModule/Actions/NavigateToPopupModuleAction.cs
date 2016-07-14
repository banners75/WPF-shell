using System.Windows;
using Common.Events;
using Infrastructure;
using Infrastructure.Navigation;
using Infrastructure.Views;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using PopupModule.Views;

namespace PopupModule.Actions
{
    public class NavigateToPopupModuleAction
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IUnityContainer _container;
       
        private readonly IViewService _viewService;

        public NavigateToPopupModuleAction(IEventAggregator eventAggregator, IUnityContainer container, IViewService viewService)
        {
            _eventAggregator = eventAggregator;
            _container = container;
            _viewService = viewService;

            _eventAggregator.GetEvent<OpenPopupModuleEvent>().Subscribe(OpenModule);
        }

        private void OpenModule(OpenModuleConfig openModuleConfig)
        {
            var view = _container.Resolve(typeof(MainPage));

            ((IView) view).Title = "opened from event";
            _viewService.ShowView(view, Regions.MultipleModelessPopupRegion, 300, 300);
        }
    }
}

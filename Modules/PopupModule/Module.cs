using System;
using Infrastructure;
using Infrastructure.Navigation;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PopupModule.Actions;
using PopupModule.Views;

namespace PopupModule
{
    public class Module : IModule 
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public Module(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<Object, MainPage>("MainPage");
            _regionManager.RegisterViewWithRegion("PopupModulesButtonContainer", typeof(PopupModuleButton));

            _container.RegisterType<NavigateToPopupModuleAction>(new ContainerControlledLifetimeManager()).Resolve<NavigateToPopupModuleAction>();
        }
    }
}

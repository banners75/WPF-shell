using System.Collections.Generic;
using Infrastructure.Views;
using Microsoft.Practices.Prism.Regions;

namespace Infrastructure.Navigation
{
    public class ViewService : IViewService
    {
        private readonly IRegionManager _regionManager;
        private readonly List<KeyValuePair<IRegion, object>> _activeViews = new List<KeyValuePair<IRegion, object>>();

        public ViewService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void ShowView(object view, string regionName, double width, double height)
        {
            if (view is IView)
            {
                var sizeableView = (IView)view;
                sizeableView.Height = height;
                sizeableView.Width = width;
            }

            var region = GetRegion(regionName);
            region.Add(view);

            _activeViews.Add(new KeyValuePair<IRegion, object>(region, view));
        }

        public void CloseAll()
        {
            foreach (var activeView in _activeViews)
            {
                activeView.Key.Remove(activeView.Value);
            }
        }

        private IRegion GetRegion(string regionName)
        {
            return _regionManager.Regions[regionName];
        }
    }
}

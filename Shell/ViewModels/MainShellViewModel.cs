using System.Windows.Input;
using Common.Events;
using Infrastructure.Navigation;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Shell.ViewModels
{
    public class MainShellViewModel : NotificationObject 
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IViewService _viewService;
        private bool _isOverlayModalDialogActive;

        public MainShellViewModel(IEventAggregator eventAggregator, IViewService viewService)
        {
            _eventAggregator = eventAggregator;
            _viewService = viewService;
            ShowPopupModuleCommand = new DelegateCommand(ShowPopupModule);
            ShowOverlayModalDialogCommand = new DelegateCommand(ShowOverlayModalDialog);
            CloseOverlayModalDialogCommand = new DelegateCommand(CloseOverlayModalDialog);
        }

        public ICommand ShowPopupModuleCommand { get; private set; }
        public ICommand ShowOverlayModalDialogCommand { get; private set; }
        public ICommand CloseOverlayModalDialogCommand { get; private set; }

        public bool IsOverlayModalDialogActive
        {
            get { return _isOverlayModalDialogActive; }
            set
            {
                _isOverlayModalDialogActive = value;
                RaisePropertyChanged(() => IsOverlayModalDialogActive);
            }
        }

        public void Closing()
        {
            _viewService.CloseAll();
        }

        private void ShowPopupModule()
        {
            _eventAggregator.GetEvent<OpenPopupModuleEvent>().Publish(new OpenModuleConfig());
        }

        private void ShowOverlayModalDialog()
        {
            IsOverlayModalDialogActive = true;
        }

        private void CloseOverlayModalDialog()
        {
            IsOverlayModalDialogActive = false;
        }
    }
}

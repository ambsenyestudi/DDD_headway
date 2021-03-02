using ELL.Desktop.UI.Notifications;
using ELL.Desktop.UI.Services.Paths;
using ELL.Desktop.UI.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ELL.Desktop.UI.ViewModels
{
    public class LoadingViewModel: ViewModelBase
    {
        private readonly IPathService pathService;
        private readonly IMessenger messenger;
        public ICommand LoadPathsCommand { get; }
        public LoadingViewModel(IPathService pathService, IMessenger messenger)
        {
            this.pathService = pathService;
            this.messenger = messenger;
            LoadPathsCommand = new RelayCommand(LoadPaths);
        }

        

        private async void LoadPaths()
        {
            await pathService.GetPaths();
            await Task.Factory.StartNew(() =>
                messenger.Send(new PageNavigated(nameof(WelcomePage))),
                CancellationToken.None, TaskCreationOptions.None, 
                TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}

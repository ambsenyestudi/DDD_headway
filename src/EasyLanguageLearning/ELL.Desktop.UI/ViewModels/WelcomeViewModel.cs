using ELL.Desktop.UI.Services;
using ELL.Desktop.UI.ViewModels.Base;
using ELL.Desktop.UI.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ELL.Desktop.UI.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        public ObservableCollection<string> PathList { get; }
        private string selectedPath;

        public string SelectedPath
        {
            get => selectedPath; 
            set 
            { 
                selectedPath = value;
                OnPropertyChanged();
                ChoosePathCommand.RaiseCanExecuteChange();
            }
        }

        public SimpleCommand ChoosePathCommand { get; }
        public WelcomeViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            PathList = new ObservableCollection<string>
            {
                "French",
                "Spanish"
            };
            ChoosePathCommand = new SimpleCommand(ChoosePath, CanChoosePath);
        }

        private bool CanChoosePath(object arg) =>
            !string.IsNullOrWhiteSpace(SelectedPath);

        private void ChoosePath(object obj) =>
            navigationService.NavigateTo(nameof(CoursePage));
    }
}

using ELL.Desktop.UI.Models;
using ELL.Desktop.UI.Notifications;
using ELL.Desktop.UI.Services;
using ELL.Desktop.UI.Services.Paths;
using ELL.Desktop.UI.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace ELL.Desktop.UI.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        private readonly IMessenger messenger;
        private readonly IMemoryCache memoryCache;

        public ObservableCollection<LearningPath> PathList { get; private set; } = new ObservableCollection<LearningPath>();
        private string selectedPath;
        public string SelectedPath
        {
            get => selectedPath; 
            set 
            { 
                selectedPath = value;
                RaisePropertyChanged();
                IsPathChoosen = !string.IsNullOrWhiteSpace(selectedPath);
            }
        }
        public ObservableCollection<string> CourseList { get; private set; }
        private string selectedCourse;
        public string SelectedCourse
        {
            get => selectedCourse;
            set
            {
                selectedCourse = value;
                RaisePropertyChanged();
            }
        }

        private bool isPathChoosen;

        public bool IsPathChoosen
        {
            get { return isPathChoosen; }
            set 
            { 
                isPathChoosen = value;
                ChoosePathCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }


        public RelayCommand ChoosePathCommand { get; }
        public RelayCommand UpdatePathListCommand { get; }

        public WelcomeViewModel(IMessenger messenger, IMemoryCache memoryCache)
        {
            this.messenger = messenger;
            this.memoryCache = memoryCache;
            ChoosePathCommand = new RelayCommand(ChoosePath, CanChoosePath);
            UpdatePathListCommand = new RelayCommand(UpdatePathList);
        }

        private void UpdatePathList()
        {
            PathList.Clear();
            if (memoryCache.TryGetValue<List<LearningPath>>("leaningPathList", out var learningPathCollection))
            {
                foreach (var learningPath in learningPathCollection)
                {
                    PathList.Add(learningPath);
                }
                
            }
        }
        
        private bool CanChoosePath() =>
            !string.IsNullOrWhiteSpace(SelectedPath);

        private void ChoosePath() =>
            messenger.Send(new PageNavigated(nameof(CoursePage)));
    }
}

using ELL.Desktop.UI.Models;
using ELL.Desktop.UI.Notifications;
using ELL.Desktop.UI.Services;
using ELL.Desktop.UI.Services.Courses;
using ELL.Desktop.UI.Services.Lessons;
using ELL.Desktop.UI.Services.Paths;
using ELL.Desktop.UI.Services.VocabularyUnits;
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
using System.Windows.Threading;

namespace ELL.Desktop.UI.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        public Dispatcher Dispatcher { get; }

        private readonly IMessenger messenger;
        private readonly IMemoryCache memoryCache;
        private readonly ICourseService courseService;
        private readonly ILessonService lessonService;
        private readonly IVocabularyService vocabularyService;

        public ObservableCollection<LearningPath> PathList { get; private set; } = new ObservableCollection<LearningPath>();
        private LearningPath selectedPath;
        public LearningPath SelectedPath
        {
            get => selectedPath; 
            set 
            { 
                selectedPath = value;
                RaisePropertyChanged();
                IsPathChoosen = selectedPath is not null;
                if (selectedPath != null)
                {
                    UpdateCourseList();
                }
            }
        }

        public ObservableCollection<Course> CourseList { get; private set; } = new ObservableCollection<Course>();
        private Course selectedCourse;
        public Course SelectedCourse
        {
            get => selectedCourse;
            set
            {
                selectedCourse = value;
                RaisePropertyChanged();
                if (selectedCourse != null)
                {
                    UpdateLessonList();
                }
            }
        }

        public ObservableCollection<Lesson> LessonList { get; private set; } = new ObservableCollection<Lesson>();
        private Lesson selectedLesson;

        public Lesson SelectedLesson
        {
            get => selectedLesson; 
            set 
            { 
                selectedLesson = value;
                RaisePropertyChanged();
                if(selectedLesson!=null)
                {
                    UpdateVocabularyList();
                }
            }
        }

        

        public ObservableCollection<Vocabulary> VocabularyList { get; private set; } = new ObservableCollection<Vocabulary>();
        private Vocabulary selectedVocabulary;

        public Vocabulary SelectedVocabulary
        {
            get => selectedVocabulary;
            set
            {
                selectedVocabulary = value;
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

        public WelcomeViewModel(IMessenger messenger, ICourseService courseService, ILessonService lessonService, IVocabularyService vocabularyService, IMemoryCache memoryCache)
        {
            Dispatcher = Dispatcher.CurrentDispatcher;
            this.messenger = messenger;
            this.memoryCache = memoryCache;
            this.courseService = courseService;
            this.lessonService = lessonService;
            this.vocabularyService = vocabularyService;
            ChoosePathCommand = new RelayCommand(ChoosePath, CanChoosePath);
            UpdatePathListCommand = new RelayCommand(UpdatePathList);
            LessonList.Add(new Lesson { Name = "No items" });
        }

        private async void UpdateVocabularyList()
        {
            var vocabularyList = await vocabularyService.GetVocabulary(SelectedLesson.Id);
            await UpdateVocabularyList(vocabularyList);
        }

        private Task UpdateVocabularyList(List<Vocabulary> vocabularyList) =>
            Task.Factory.StartNew(() =>
            {
                VocabularyList.Clear();
                foreach (var vocabularyItem in vocabularyList)
                {
                    VocabularyList.Add(vocabularyItem);
                }
            },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext());

        private async void UpdateLessonList()
        {
            var lessonList = await lessonService.GetLessons(SelectedCourse.Id);
            await UpdateLessonListAsync(lessonList);
        }

        private Task UpdateLessonListAsync(List<Lesson> lessonList) =>
            Task.Factory.StartNew(() =>
            {

                LessonList.Clear();
                Dispatcher.BeginInvoke(() =>
                {
                    foreach (var lesson in lessonList)
                    {
                        LessonList.Add(lesson);
                    }
                });
            },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext());

        private async void UpdateCourseList()
        {
            var courses = await courseService.GetCourses(SelectedPath.Id);
            await UpdateCourseListAsync(courses);
        }
        private Task UpdateCourseListAsync(List<Course> courseList) =>
            Task.Factory.StartNew(()=> 
                {
                    CourseList.Clear();
                    foreach (var course in courseList)
                    {
                        CourseList.Add(course);
                    }
                },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext());
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
            SelectedPath is not null;

        private void ChoosePath() =>
            messenger.Send(new PageNavigated(nameof(CoursePage)));
    }
}

using ELL.Desktop.UI.Services;
using ELL.Desktop.UI.ViewModels.Base;
using System.Collections.ObjectModel;

namespace ELL.Desktop.UI.ViewModels
{
    public class CoursesViewModel: ViewModelBase
    {
        public ObservableCollection<string> CourseList { get; }
        public SimpleCommand ChooseCourseCommand { get; }

        private string selectedCourse;
        private INavigationService navigationService;

        public string SelectedCourse
        {
            get => selectedCourse;
            set
            {
                selectedCourse = value;
                OnPropertyChanged();
                ChooseCourseCommand.RaiseCanExecuteChange();
            }
        }
        public CoursesViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            CourseList = new ObservableCollection<string>
            {
                "French 1"
            };
            ChooseCourseCommand = new SimpleCommand(ChooseCourse, CanChooseCourse);
        }

        private bool CanChooseCourse(object arg) =>
            !string.IsNullOrWhiteSpace(SelectedCourse);

        private void ChooseCourse(object obj)
        {
            //todo
        }
    }
}

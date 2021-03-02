using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace ELL.Desktop.UI.ViewModels
{
    public class CoursesViewModel: ViewModelBase
    {
        private readonly IMessenger messenger;
        public ObservableCollection<string> CourseList { get; }
        public RelayCommand ChooseCourseCommand { get; }

        private string selectedCourse;

        public string SelectedCourse
        {
            get => selectedCourse;
            set
            {
                selectedCourse = value;
                RaisePropertyChanged();
                ChooseCourseCommand.RaiseCanExecuteChanged();
            }
        }
        public CoursesViewModel(IMessenger messenger)
        {
            this.messenger = messenger;
            CourseList = new ObservableCollection<string>
            {
                "French 1"
            };
            ChooseCourseCommand = new RelayCommand(ChooseCourse, CanChooseCourse);
        }

        private bool CanChooseCourse() =>
            !string.IsNullOrWhiteSpace(SelectedCourse);

        private void ChooseCourse()
        {
            //todo ediator send pagenavigated
        }
    }
}

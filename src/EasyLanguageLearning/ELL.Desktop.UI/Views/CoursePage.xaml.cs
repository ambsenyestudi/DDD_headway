using ELL.Desktop.UI.ViewModels;
using ELL.Desktop.UI.ViewModels.Base;
using System.Windows.Controls;

namespace ELL.Desktop.UI.Views
{
    /// <summary>
    /// Interaction logic for CoursePage.xaml
    /// </summary>
    public partial class CoursePage : Page, INavigationPage
    {
        public string PageName => nameof(CoursePage);
        public CoursePage(CoursesViewModel coursesViewModel)
        {
            DataContext = coursesViewModel;
            InitializeComponent();
        }

       
    }
}

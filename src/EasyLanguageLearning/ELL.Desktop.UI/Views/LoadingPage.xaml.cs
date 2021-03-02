using ELL.Desktop.UI.ViewModels;
using ELL.Desktop.UI.ViewModels.Base;
using System.Windows.Controls;

namespace ELL.Desktop.UI.Views
{
    /// <summary>
    /// Interaction logic for LoadingPage.xaml
    /// </summary>
    public partial class LoadingPage : Page, INavigationPage
    {
        public string PageName => nameof(LoadingPage);
        public LoadingPage(LoadingViewModel lodingViewModel)
        {
            DataContext = lodingViewModel;
            InitializeComponent();
        }
    }
}

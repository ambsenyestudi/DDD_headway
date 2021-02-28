using ELL.Desktop.UI.ViewModels;
using ELL.Desktop.UI.ViewModels.Base;
using System.Windows.Controls;

namespace ELL.Desktop.UI.Views
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Page, INavigationPage
    {
        public string PageName => nameof(WelcomePage);
        public WelcomePage(WelcomeViewModel welcomeViewModel)
        {
            this.DataContext = welcomeViewModel;
            InitializeComponent();
        }

        
    }
}

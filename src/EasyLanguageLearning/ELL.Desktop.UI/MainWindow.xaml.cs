using ELL.Desktop.UI.ViewModels;
using System.Windows;

namespace ELL.Desktop.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            this.DataContext = mainViewModel;
            InitializeComponent();
        }
    }
}

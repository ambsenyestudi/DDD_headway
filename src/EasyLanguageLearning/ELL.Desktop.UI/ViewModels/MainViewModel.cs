using ELL.Desktop.UI.Notifications;
using ELL.Desktop.UI.ViewModels.Base;
using ELL.Desktop.UI.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace ELL.Desktop.UI.ViewModels
{
    //event to command no more now
    //https://github.com/microsoft/XamlBehaviorsWpf
    public class MainViewModel: ViewModelBase
    {
        private readonly string homeViewName;
        private readonly IMessenger messenger;
        private readonly IEnumerable<INavigationPage> pageCollection;
        private Page currentPage;
        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel(IEnumerable<INavigationPage> pages, IMessenger messenger)
        {
            this.messenger = messenger;
            pageCollection = pages;
            homeViewName = nameof(WelcomePage);
            
            InitNavigation();
        }

        private void InitNavigation()
        {
            
            UpdateCurrentPage(nameof(LoadingPage));
            messenger.Register<PageNavigated>(this, 
                page => UpdateCurrentPage(page.PageName));
        }

        private void UpdateCurrentPage(string pageName)
        {
            if (!IsCurrentPage(pageName))
            {

                var navPage = pageCollection.First(pa => pa.PageName == pageName);

                if (navPage != null)
                {
                    var currPage = navPage as Page;
                    CurrentPage = currPage;
                }
            }

        }



        private bool IsCurrentPage(string pageName) =>
            CurrentPage !=null &&
            ((INavigationPage)CurrentPage).PageName == pageName; 
    }
}

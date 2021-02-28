using ELL.Desktop.UI.Services;
using ELL.Desktop.UI.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace ELL.Desktop.UI.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        private readonly ITextService textService;
        private readonly INavigationService navigationService;
        private readonly IEnumerable<INavigationPage> pageCollection;
        private Page currentPage;
        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                if (currentPage != value)
                {
                    currentPage = value;
                    OnPropertyChanged();

                }
            }
        }

        public MainViewModel(INavigationService navigationService, ITextService textService, IEnumerable<INavigationPage> pages)
        {
            this.textService = textService;
            this.navigationService = navigationService;
            this.pageCollection = pages;
            InitNavService();
            
        }
        private void InitNavService()
        {
            UpdateCurrentPage(navigationService.CurrentPage);
            navigationService.NavigatedPage += NavigationStack_PageNavigated;
            navigationService.InitPageNames(pageCollection.Select(x => x.PageName));
            
            
            
        }

        private void NavigationStack_PageNavigated(string pageName) =>
            UpdateCurrentPage(pageName);

        private void UpdateCurrentPage(string pageName)
        {

            var navPage = pageCollection.First(pa => pa.PageName == pageName);

            if (navPage != null)
            {
                var currPage = navPage as Page;
                CurrentPage = currPage;
            }

        }
        protected override void Dispose()
        {
            navigationService.NavigatedPage -= NavigationStack_PageNavigated;
        }
    }
}

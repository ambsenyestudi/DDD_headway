using ELL.Desktop.UI.ViewModels.Base;
using ELL.Desktop.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ELL.Desktop.UI.Services
{
    public class NavigationService : INavigationService
    {
        private IEnumerable<string> pageCollection;
        private readonly string homePage;
        private IList<string> stack = new List<string>();

        public event Action<string> NavigatedPage;

        public string CurrentPage => stack.Last();

        public NavigationService()
        {
            homePage = nameof(WelcomePage);
            stack.Add(homePage);
        }
        public void InitPageNames(IEnumerable<string> pageName)
        {
            if(pageCollection == null)
            {
                pageCollection = new List<string>();
            }
            pageCollection = pageCollection.Union(pageName);
        }
        public void NavigateTo(string pageName)
        {
            if (stack.Last() != pageName && pageCollection.Any(pa => pa == pageName))
            {
                var page = pageCollection.First(pa => pa == pageName);
                stack.Add(page);
                NavigatedPage?.Invoke(CurrentPage);
            }
        }
    }
}

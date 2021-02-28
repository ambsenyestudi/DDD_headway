using System;
using System.Collections.Generic;

namespace ELL.Desktop.UI.Services
{
    public interface INavigationService
    {
        event Action<string> NavigatedPage;
        string CurrentPage { get; }
        void NavigateTo(string pageName);
        void InitPageNames(IEnumerable<string> pageName);

    }
}

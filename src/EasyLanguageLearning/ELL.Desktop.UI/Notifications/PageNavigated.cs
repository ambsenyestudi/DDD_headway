namespace ELL.Desktop.UI.Notifications
{
    public class PageNavigated
    {
        public string PageName { get; }
        public PageNavigated(string pageName)
        {
            PageName = pageName;
        }
    }
}

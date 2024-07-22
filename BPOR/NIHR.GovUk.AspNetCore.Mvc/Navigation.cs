namespace NIHR.GovUk
{
    public class Navigation
    {
        public IList<NavigationItem> Items { get; set; } = [];

        public void Add(string label, Uri uri)
        {
            Items.Add(new NavigationItem { Label = label, Uri = uri });
        }
        
        public void Add(NavigationItem item)
        {
            Items.Add(item);
        }
    }

    public class NavigationItem
    {
        public string Label { get; set; } = "#";
        public Uri Uri { get; set; } = new Uri("#", UriKind.Relative);
    }
}
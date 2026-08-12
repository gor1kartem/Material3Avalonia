using Avalonia.Interactivity;

namespace MaterialTheme.CustomControls;

public class NavigationRailSelectionChangedEventArgs : RoutedEventArgs
{
    public NavigationItem SelectedItem { get; private set; }

    public NavigationRailSelectionChangedEventArgs(RoutedEvent routedEvent, object? source, NavigationItem item) : base(routedEvent, source)
    {
        SelectedItem = item;
    }
}
using Avalonia;
using Avalonia.Controls;

namespace MaterialTheme.CustomControls;

public class NavigationItem : AvaloniaObject
{
    public static readonly StyledProperty<string> LabelProperty =
        AvaloniaProperty.Register<NavigationItem, string>(nameof(Label));
    
    public string Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public static readonly StyledProperty<Control> IconProperty = AvaloniaProperty.Register<NavigationItem, Control>(nameof(Icon));

    public Control Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly StyledProperty<string> TagProperty =
        AvaloniaProperty.Register<NavigationItem, string>(nameof(Tag));

    public string Tag
    {
        get => GetValue(TagProperty);
        set => SetValue(TagProperty, value);
    }
}
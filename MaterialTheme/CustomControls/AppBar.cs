using Avalonia;
using Avalonia.Controls;

namespace MaterialTheme.CustomControls;

public class AppBar : ContentControl
{
    public static readonly StyledProperty<object?> HeaderProperty = AvaloniaProperty.Register<AppBar, object?>(
        nameof(Header));

    public object? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }
}
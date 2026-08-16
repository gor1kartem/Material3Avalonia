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

    public static readonly StyledProperty<bool> IsBackButtonVisibleProperty = AvaloniaProperty.Register<AppBar, bool>(
        nameof(IsBackButtonVisible), true);

    public bool IsBackButtonVisible
    {
        get => GetValue(IsBackButtonVisibleProperty);
        set => SetValue(IsBackButtonVisibleProperty, value);
    }

    public static readonly StyledProperty<bool> IsHeaderVisibleProperty = AvaloniaProperty.Register<AppBar, bool>(
        nameof(IsHeaderVisible), true);

    public bool IsHeaderVisible
    {
        get => GetValue(IsHeaderVisibleProperty);
        set => SetValue(IsHeaderVisibleProperty, value);
    }
}
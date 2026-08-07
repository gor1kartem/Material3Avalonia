using Avalonia;
using Avalonia.Controls;

namespace MaterialTheme.CustomControls;

public class AppBar : ContentControl
{
    public static readonly StyledProperty<string?> TitleProperty = AvaloniaProperty.Register<AppBar, string?>(
        nameof(Title));

    public string? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
}
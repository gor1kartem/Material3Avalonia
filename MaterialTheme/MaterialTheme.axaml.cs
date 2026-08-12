using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace MaterialTheme;

public class MaterialTheme : Styles
{
    public MaterialTheme()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
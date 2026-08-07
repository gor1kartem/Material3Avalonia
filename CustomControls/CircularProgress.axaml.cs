using Avalonia;
using Avalonia.Controls.Primitives;

namespace MaterialTheme.CustomControls;

public class CircularProgress : TemplatedControl
{
    public CircularProgress()
    {
        
    }

    public static readonly StyledProperty<double> StrokeThicknessProperty =
        AvaloniaProperty.Register<CircularProgress, double>(nameof(StrokeThickness));

    public double StrokeThickness
    {
        get => GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }
}
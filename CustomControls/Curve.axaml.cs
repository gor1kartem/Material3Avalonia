using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace MaterialTheme.CustomControls;

public class Curve : Control
{
    public static readonly StyledProperty<IBrush?> StrokeProperty =
        AvaloniaProperty.Register<Curve, IBrush?>(nameof(Stroke), Brushes.OliveDrab);

    public static readonly StyledProperty<double> StrokeThicknessProperty =
        AvaloniaProperty.Register<Curve, double>(nameof(StrokeThickness), 8);

    public static readonly StyledProperty<double> WaveLengthProperty =
        AvaloniaProperty.Register<Curve, double>(nameof(WaveLength), 40);

    public static readonly StyledProperty<double> WaveHeightProperty =
        AvaloniaProperty.Register<Curve, double>(nameof(WaveHeight), 3);

    public static readonly StyledProperty<double> OffsetProperty =
        AvaloniaProperty.Register<Curve, double>(nameof(Offset));

    public static readonly StyledProperty<Orientation> OrientationProperty =
        AvaloniaProperty.Register<Curve, Orientation>(nameof(Orientation));

    static Curve()
    {
        AffectsRender<Curve>(
            StrokeProperty,
            StrokeThicknessProperty,
            WaveLengthProperty,
            WaveHeightProperty,
            OffsetProperty,
            OrientationProperty);

        AffectsMeasure<Curve>(
            StrokeThicknessProperty,
            WaveHeightProperty,
            OrientationProperty);
    }

    public IBrush? Stroke
    {
        get => GetValue(StrokeProperty);
        set => SetValue(StrokeProperty, value);
    }

    public double StrokeThickness
    {
        get => GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }

    public double WaveLength
    {
        get => GetValue(WaveLengthProperty);
        set => SetValue(WaveLengthProperty, value);
    }

    public double WaveHeight
    {
        get => GetValue(WaveHeightProperty);
        set => SetValue(WaveHeightProperty, value);
    }

    public double Offset
    {
        get => GetValue(OffsetProperty);
        set => SetValue(OffsetProperty, value);
    }

    public Orientation Orientation
    {
        get => GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        var desiredThickness = Math.Max(0, StrokeThickness) + Math.Max(0, WaveHeight) * 2;
        return Orientation == Orientation.Horizontal
            ? new Size(0, desiredThickness)
            : new Size(desiredThickness, 0);
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        var width = Bounds.Width;
        var height = Bounds.Height;
        var stroke = Stroke;
        var strokeThickness = Math.Max(0, StrokeThickness);
        var waveLength = Math.Max(1, WaveLength);

        if (width <= 0 || height <= 0 || stroke is null || strokeThickness <= 0)
        {
            return;
        }

        var halfStroke = strokeThickness / 2;
        var left = Math.Min(halfStroke, width / 2);
        var right = Math.Max(left, width - halfStroke);
        var centerY = height / 2;
        var maxWaveHeight = Math.Max(0, (height - strokeThickness) / 2);
        var waveHeight = Math.Min(Math.Max(0, WaveHeight), maxWaveHeight);
        var pen = new Pen(stroke, strokeThickness, null, PenLineCap.Round, PenLineJoin.Round, 10);
        var geometry = new StreamGeometry();

        using (var geometryContext = geometry.Open())
        {
            if (Orientation == Orientation.Horizontal)
            {
                DrawHorizontal(geometryContext, left, right, centerY, waveHeight, waveLength);
            }
            else
            {
                DrawVertical(geometryContext, width, height, strokeThickness, waveHeight, waveLength);
            }
        }

        context.DrawGeometry(null, pen, geometry);
    }

    private void DrawHorizontal(
        StreamGeometryContext geometryContext,
        double left,
        double right,
        double centerY,
        double waveHeight,
        double waveLength)
    {
        geometryContext.BeginFigure(new Point(left, GetY(left, centerY, waveHeight, waveLength)), false);

        if (waveHeight > 0)
        {
            for (var x = left + 4; x < right; x += 4)
            {
                geometryContext.LineTo(new Point(x, GetY(x, centerY, waveHeight, waveLength)), true);
            }
        }

        geometryContext.LineTo(new Point(right, GetY(right, centerY, waveHeight, waveLength)), true);
        geometryContext.EndFigure(false);
    }

    private void DrawVertical(
        StreamGeometryContext geometryContext,
        double width,
        double height,
        double strokeThickness,
        double waveHeight,
        double waveLength)
    {
        var halfStroke = strokeThickness / 2;
        var top = Math.Min(halfStroke, height / 2);
        var bottom = Math.Max(top, height - halfStroke);
        var centerX = width / 2;
        var maxWaveHeight = Math.Max(0, (width - strokeThickness) / 2);
        var clampedWaveHeight = Math.Min(waveHeight, maxWaveHeight);

        geometryContext.BeginFigure(new Point(GetX(bottom, centerX, clampedWaveHeight, waveLength), bottom), false);

        if (clampedWaveHeight > 0)
        {
            for (var y = bottom - 4; y > top; y -= 4)
            {
                geometryContext.LineTo(new Point(GetX(y, centerX, clampedWaveHeight, waveLength), y), true);
            }
        }

        geometryContext.LineTo(new Point(GetX(top, centerX, clampedWaveHeight, waveLength), top), true);
        geometryContext.EndFigure(false);
    }

    private double GetY(double x, double centerY, double waveHeight, double waveLength)
    {
        var phase = (x + Offset) / waveLength * Math.Tau;
        return centerY + Math.Sin(phase) * waveHeight;
    }

    private double GetX(double y, double centerX, double waveHeight, double waveLength)
    {
        var phase = (y + Offset) / waveLength * Math.Tau;
        return centerX + Math.Sin(phase) * waveHeight;
    }
}

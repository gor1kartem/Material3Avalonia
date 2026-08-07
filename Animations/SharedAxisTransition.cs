using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Transformation;
using Avalonia.Styling;

namespace MaterialTheme.Animations;

public class SharedAxisTransition : IPageTransition
{
    public TimeSpan Duration { get; set; } = TimeSpan.FromMilliseconds(300);
    public SlideAxis Direction = SlideAxis.X;

    public bool Forward { get; set; } = true;
    public Easing Easing { get; set; } = new SplineEasing(0.4, 0.0, 0.2, 1.0);

    public async Task Start(
        Visual? from,
        Visual? to,
        bool forward,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }

        StyledProperty<double> translateProperty;
        if (Direction == SlideAxis.X) translateProperty = TranslateTransform.XProperty;
        if (Direction == SlideAxis.Y) translateProperty = TranslateTransform.YProperty;
        else translateProperty = TranslateTransform.XProperty;
        
        var firstPageOffset = forward ? -30d : 30d;
        var secondPageStartOffset = -firstPageOffset;
        Animation firstPageTransition = new()
        {
            Duration = Duration, Easing = Easing, FillMode = FillMode.Forward,
            Children =
            {
                new KeyFrame()
                {
                    Cue = new Cue(0d),
                    Setters = { new Setter(Visual.OpacityProperty, 1d), new Setter(translateProperty, 0d) }
                },
                new KeyFrame(){Cue = new Cue(0.3d), Setters = { new Setter(Visual.OpacityProperty, 0d), new Setter(translateProperty, firstPageOffset) }}
            }
        };
        
        Animation secondPageTransition = new()
        {
            Duration = Duration, Easing = Easing, FillMode = FillMode.Forward,
            Children =
            {
                new KeyFrame()
                {
                    Cue = new Cue(0d),
                    Setters = { new Setter(Visual.OpacityProperty, 0d), new Setter(translateProperty, secondPageStartOffset) }
                },
                new KeyFrame() {Cue = new Cue(0.3d), Setters = { new Setter(Visual.OpacityProperty, 0d) }}
                ,
                new KeyFrame(){Cue = new Cue(1d), Setters = { new Setter(Visual.OpacityProperty, 1d), new Setter(translateProperty, 0d) }}
            }
        };

        List<Task> tasks = new() {firstPageTransition.RunAsync(from, cancellationToken), secondPageTransition.RunAsync(to, cancellationToken)};
        await Task.WhenAll(tasks);
    }
    
    public enum SlideAxis
    {
        X, Y
    }
}
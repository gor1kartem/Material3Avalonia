using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Styling;

namespace MaterialTheme.CustomControls;

[TemplatePart("PART_Border", typeof(Border))]
[TemplatePart("PART_ContentPresenter", typeof(ContentPresenter))]
public class MaterialExpander : ContentControl
{
    private ContentPresenter? _contentPresenter;
    private CancellationTokenSource? _animationCancellationTokenSource;

    public static readonly StyledProperty<bool> IsExpandedProperty = AvaloniaProperty.Register<MaterialExpander, bool>(
        nameof(IsExpanded), defaultBindingMode:BindingMode.TwoWay);

    public bool IsExpanded
    {
        get => GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public static readonly StyledProperty<string> HeaderProperty = AvaloniaProperty.Register<MaterialExpander, string>(
        nameof(Header));

    public string Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public static readonly StyledProperty<double> HeaderHeightProperty = AvaloniaProperty.Register<MaterialExpander, double>(
        nameof(HeaderHeight));

    public double HeaderHeight
    {
        get => GetValue(HeaderHeightProperty);
        set => SetValue(HeaderHeightProperty, value);
    }
    
    
    public static readonly RoutedEvent<RoutedEventArgs> IsExpandedChangedEvent =
        RoutedEvent.Register<MaterialExpander, RoutedEventArgs>(nameof(IsExpandedChanged), RoutingStrategies.Bubble);
    
    public event EventHandler<RoutedEventArgs> IsExpandedChanged
    {
        add => AddHandler(IsExpandedChangedEvent, value);
        remove => RemoveHandler(IsExpandedChangedEvent, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _contentPresenter = e.NameScope.Find<ContentPresenter>("PART_ContentPresenter");
        UpdateContentPresenterState(true);
    }

    protected override async void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == IsExpandedProperty)
        {
            await OnIsExpandedChangedAsync();
        }
    }

    private async Task OnIsExpandedChangedAsync()
    {
        RaiseEvent(new RoutedEventArgs(IsExpandedChangedEvent));
        if (_contentPresenter is null)
        {
            return;
        }

        _animationCancellationTokenSource?.Cancel();
        _animationCancellationTokenSource?.Dispose();
        _animationCancellationTokenSource = new CancellationTokenSource();

        CancellationToken cancellationToken = _animationCancellationTokenSource.Token;
        double startHeight = _contentPresenter.Bounds.Height;
        double endHeight = IsExpanded ? GetDesiredContentHeight() : 0d;
        double startOpacity = _contentPresenter.Opacity;
        double endOpacity = IsExpanded ? 1d : 0d;

        _contentPresenter.IsVisible = true;
        _contentPresenter.Height = startHeight;

        Animation animation = new()
        {
            Duration = TimeSpan.FromMilliseconds(250),
            Easing = new CubicEaseOut(),
            FillMode = FillMode.Forward,
            Children =
            {
                new KeyFrame
                {
                    Cue = new Cue(0d),
                    Setters =
                    {
                        new Setter(HeightProperty, startHeight),
                        new Setter(OpacityProperty, startOpacity)
                    }
                },
                new KeyFrame
                {
                    Cue = new Cue(1d),
                    Setters =
                    {
                        new Setter(HeightProperty, endHeight),
                        new Setter(OpacityProperty, endOpacity)
                    }
                }
            }
        };

        try
        {
            await animation.RunAsync(_contentPresenter, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            return;
        }

        if (!cancellationToken.IsCancellationRequested)
        {
            UpdateContentPresenterState(true);
        }
    }
    
    private double GetDesiredContentHeight()
    {
        if (_contentPresenter is null)
        {
            return 0d;
        }

        double previousHeight = _contentPresenter.Height;
        _contentPresenter.Height = double.NaN;
        _contentPresenter.IsVisible = true;
        _contentPresenter.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        double desiredHeight = _contentPresenter.DesiredSize.Height;
        _contentPresenter.Height = previousHeight;

        return desiredHeight;
    }

    private void UpdateContentPresenterState(bool keepAutoHeightWhenExpanded)
    {
        if (_contentPresenter is null)
        {
            return;
        }

        _contentPresenter.IsVisible = IsExpanded;
        _contentPresenter.Opacity = IsExpanded ? 1d : 0d;
        _contentPresenter.Height = IsExpanded && keepAutoHeightWhenExpanded ? double.NaN : 0d;
    }
}

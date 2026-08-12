using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.VisualTree;

namespace MaterialTheme.CustomControls.Settings;

[TemplatePart("PART_ContentPresenter", typeof(ContentPresenter))]
public class SettingsItem : Button
{
    private ContentPresenter? _contentPresenter;
    public static readonly StyledProperty<object?> IconProperty = AvaloniaProperty.Register<SettingsItem, object?>(
        nameof(Icon));

    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly StyledProperty<string?> LabelProperty = AvaloniaProperty.Register<SettingsItem, string?>(
        nameof(Label));

    public string? Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public static readonly StyledProperty<string?> DescriptionProperty = AvaloniaProperty.Register<SettingsItem, string?>(
        nameof(Description));

    public string? Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _contentPresenter = e.NameScope.Find<ContentPresenter>("PART_ContentPresenter");
        // OnContentChanged();
    }

    // protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    // {
    //     base.OnPropertyChanged(change);
    //     if (change.Property == ContentProperty)
    //     {
    //         OnContentChanged();
    //     }
    // }

    // private void OnContentChanged()
    // {
    //     _contentPresenter?.IsVisible = Content is not ContentPage;
    // }

    protected override async void OnClick()
    {
        base.OnClick();
        if (this.Content is ToggleSwitch toggleSwitch)
        {
            toggleSwitch.IsChecked = !toggleSwitch.IsChecked;
        }
        else if (this.Content is ComboBox comboBox && comboBox.IsEnabled)
        {
            comboBox.IsDropDownOpen = true;
        }
        else if (Content is ContentPage contentPage)
        {
            NavigationPage? navigationPage = this.FindAncestorOfType<NavigationPage>();
            // if (navigationPage is not null)
            // {
            //     await navigationPage.PushAsync(contentPage);
            // }
        }
    }
}
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Styling;

namespace MaterialTheme.CustomControls;

[TemplatePart("PART_ListBox", typeof(ListBox))]
public class ButtonGroup : TemplatedControl
{
    private UniformGrid? _uniformGrid;
    
    public static readonly DirectProperty<ButtonGroup, IEnumerable<string>> ItemsProperty =
        AvaloniaProperty.RegisterDirect<ButtonGroup, IEnumerable<string>>(nameof(Items), group => group.Items, (group, items) => group.Items = items);

    private IEnumerable<string> _items;

    public static readonly DirectProperty<ButtonGroup, string> SelectedItemProperty =
        AvaloniaProperty.RegisterDirect<ButtonGroup, string>(nameof(SelectedItem), group => group.SelectedItem, (group, s) => group.SelectedItem = s);

    private string _selectedItem;
    public string SelectedItem
    {
        get => _selectedItem;
        set => SetAndRaise(SelectedItemProperty, ref _selectedItem, value);
    }
    public IEnumerable<string> Items
    {
        get => _items;
        set => SetAndRaise(ItemsProperty, ref _items, value);
    }

    public ButtonGroup()
    {
        Items = new[] { "Hello", "jonkler" };
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == ItemsProperty)
        {
            OnItemsUpdate();
        }
        else if (change.Property == SelectedItemProperty)
        {
            
        }
    }

    private void UpdateButtonState()
    {
        if (_uniformGrid is null) return;

        foreach (var button in _uniformGrid.Children.OfType<ToggleButton>())
        {
            // Сравниваем контент кнопки с выбранным элементом
            if (button.Content is string content && content == SelectedItem)
            {
                button.IsChecked = true;
                button.IsEnabled = false; // Отключаем, чтобы показать выбор
            }
            else
            {
                button.IsChecked = false;
                button.IsEnabled = true; // Включаем остальные
            }
        }
    }

    private void OnItemsUpdate()
    {
        if (_uniformGrid != null)
        {
            _uniformGrid.Children.Clear();
            _uniformGrid.Columns = Items.Count();
            
            int i = 0;
            foreach (var item in Items)
            {
                var toggleButton = new ToggleButton() { Content = item, HorizontalAlignment = HorizontalAlignment.Stretch};
                
                if (i == 0)
                {
                    if (Application.Current.TryFindResource("FirstButtonGroupToggleButtonStyle", out var firstStyle))
                    {
                        if (firstStyle is ControlTheme buttonStyle)
                        {
                            toggleButton.Theme = buttonStyle;
                        }
                    }
                }
                else if (i == Items.Count() - 1)
                {
                    if (Application.Current.TryFindResource("LastButtonGroupToggleButtonStyle", out var lastStyle))
                    {
                        if (lastStyle is ControlTheme buttonStyle)
                        {
                            toggleButton.Theme = buttonStyle;
                        }
                    }
                }
                else
                {
                    if (Application.Current.TryFindResource("ButtonGroupToggleButtonStyle", out var style))
                    {
                        if (style is ControlTheme buttonStyle)
                        {
                            toggleButton.Theme = buttonStyle;
                        }
                    }
                }

                toggleButton.Click += OnButtonClick;
                _uniformGrid.Children.Add(toggleButton);
                i++;
            }
        }
        UpdateButtonState();
        
    }

    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton toggleButton)
        {
            toggleButton.IsEnabled = false;
            SelectedItem = toggleButton.Content as string;
            foreach (var button in _uniformGrid.Children.OfType<ToggleButton>())
            {
                if (toggleButton != button)
                {
                    button.IsEnabled = true;
                    button.IsChecked = false;
                }
            }
        }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _uniformGrid = e.NameScope.Find<UniformGrid>("PART_UniformGrid");
        OnItemsUpdate();
    }
}
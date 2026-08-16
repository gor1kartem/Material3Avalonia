using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SampleApp.Views;

public partial class EmptyPage : ContentPage
{
    public EmptyPage()
    {
        InitializeComponent();
    }

    private async void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        await Navigation.PopAsync();
    }
}
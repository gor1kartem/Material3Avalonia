using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SampleApp.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void Button_NavigateToEmptyPage(object? sender, RoutedEventArgs e)
    {
        await Navigation.PushAsync(new EmptyPage());
    }
}
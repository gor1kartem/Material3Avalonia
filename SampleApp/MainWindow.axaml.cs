using Avalonia.Controls;
using Avalonia.Interactivity;
using SampleApp.Views;

namespace SampleApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void NavigationPage_OnLoaded(object? sender, RoutedEventArgs e)
    {
        await NavigationPage.PushAsync(new MainPage());
    }
}
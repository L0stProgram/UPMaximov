using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace UPPrivalov;

public partial class ResultsWindow : Window
{
    public ResultsWindow()
    {
        InitializeComponent();
    }

    private void Result1(object? sender, RoutedEventArgs e)
    {
        Result1Win w = new Result1Win();
        w.Show();
        this.Close();
    }
    
    private void Back(object? sender, RoutedEventArgs e)
    {
        MenuWindow mwin = new MenuWindow();
        mwin.Show();
        this.Close();
    }
}
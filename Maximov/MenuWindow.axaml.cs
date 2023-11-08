using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using UPPrivalov.SGroups;
using UPPrivalov.Students;
using UPPrivalov.StudentsInGroups;
using UPPrivalov.Teachers;

namespace UPPrivalov;

public partial class MenuWindow : Window
{
    public MenuWindow()
    {
        InitializeComponent();
    }

    private void CoursesButton(object? sender, RoutedEventArgs e)
    {
        CourseWindow cwin = new CourseWindow();
        cwin.Show();
        this.Close();
    }

    private void AttendancesButton(object? sender, RoutedEventArgs e)
    {
        AttendanceWindow awin = new AttendanceWindow();
        awin.Show();
        this.Close();
    }

    private void PaymentsButton(object? sender, RoutedEventArgs e)
    {
        PaymentsWindow awin = new PaymentsWindow();
        awin.Show();
        this.Close();
    }

    private void StudentsButton(object? sender, RoutedEventArgs e)
    {
        StudentsWindow awin = new StudentsWindow();
        awin.Show();
        this.Close();
    }

    private void ExitButton(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void TeachersButton(object? sender, RoutedEventArgs e)
    {
        TeachersWindow awin = new TeachersWindow();
        awin.Show();
        this.Close();
    }

    private void StudentsInGroupButton(object? sender, RoutedEventArgs e)
    {
        StudentsInGroupsWindow awin = new StudentsInGroupsWindow();
        awin.Show();
        this.Close();
    }

    private void GroupsButton(object? sender, RoutedEventArgs e)
    {
        SGroupsWindow win = new SGroupsWindow();
        win.Show();
        this.Close();
    }

    private void ResultsButton(object? sender, RoutedEventArgs e)
    {
        ResultsWindow win = new ResultsWindow();
        win.Show();
        this.Close();
    }
}
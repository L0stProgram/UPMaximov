using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using UPPrivalov.Courses;

namespace UPPrivalov;

public partial class AttendanceWindow : Window
{
    private string _constring =
        "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659989;UID=sql12659989;PASSWORD=nDB33bCp4Y;";
    
    private List<AttendanceClass> _attendances;
    private MySqlConnection _connection;
    private string fulltable = "SELECT Attendance_ID, Attendance.Schedule, Schedules.Day, Attendance.Student FROM Attendance " +
                               "JOIN Schedules on Attendance.Schedule = Schedules.Schedule_ID";
    
    public AttendanceWindow()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _attendances = new List<AttendanceClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new AttendanceClass()
            {
                AttendanceID = reader.GetInt32("Attendance_ID"),
                AttendanceDay = reader.GetDateTime("Day"),
                AttendanceStudent = reader.GetString("Student")
            };
            _attendances.Add(current);
        }
        _connection.Close();
        AttendanceDataGrid.ItemsSource = _attendances;
    }
    
    private void Back(object? sender, RoutedEventArgs e)
    {
        MenuWindow mwin = new MenuWindow();
        mwin.Show();
        this.Close();
    }
}
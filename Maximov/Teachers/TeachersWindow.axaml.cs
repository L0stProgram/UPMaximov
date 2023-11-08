using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using UPPrivalov.Courses;

namespace UPPrivalov.Teachers;

public partial class TeachersWindow : Window
{
    private string _constring =
        "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659989;UID=sql12659989;PASSWORD=nDB33bCp4Y;";
    
    private List<TeacherClass> _teachers;
    private MySqlConnection _connection;
    private string fulltable = "SELECT Teacher_ID, TeacherName, Surname, LastName, Birthday, Phone_Number, Email FROM Teachers";
    
    public TeachersWindow()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _teachers = new List<TeacherClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new TeacherClass()
            {
                TeacherID = reader.GetInt32("Teacher_ID"),
                TeacherName = reader.GetString("TeacherName"),
                TeacherSurname = reader.GetString("Surname"),
                TeacherLastName = reader.GetString("LastName"),
                TeacherBirthday = reader.GetDateTime("Birthday"),
                TeacherPhone = reader.GetString("Phone_Number"),
                TeacherEmail = reader.GetString("Email")
            };
            _teachers.Add(current);
        }
        _connection.Close();
        TeachersDataGrid.ItemsSource = _teachers;
    }

    private void Back(object? sender, RoutedEventArgs e)
    {
        MenuWindow mwin = new MenuWindow();
        mwin.Show();
        this.Close();
    }

    private void Add(object? sender, RoutedEventArgs e)
    {
        TeacherAddWindow mwin = new TeacherAddWindow();
        mwin.Show();
        this.Close();
    }
}
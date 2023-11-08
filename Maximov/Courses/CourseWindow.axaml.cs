using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using UPPrivalov.Courses;

namespace UPPrivalov;

public partial class CourseWindow : Window
{
    private string _constring =
        "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659989;UID=sql12659989;PASSWORD=nDB33bCp4Y;";
    
    private List<CourseClass> _courses;
    private MySqlConnection _connection;
    private string fulltable = "SELECT Course_ID, Course_Name, Course_Description, Courses.Language_Study, Language_Study.Language_Study_Name, Price FROM Courses " +
                               "JOIN Language_Study on Courses.Language_Study = Language_Study.Language_Study_ID";
    
    public CourseWindow()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _courses = new List<CourseClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new CourseClass()
            {
                CourseID = reader.GetInt32("Course_ID"),
                CourseName = reader.GetString("Course_Name"),
                CourseDesc = reader.GetString("Course_Description"),
                LanguageStudy = reader.GetString("Language_Study_Name"),
                Price = reader.GetDouble("Price")
            };
            _courses.Add(current);
        }
        _connection.Close();
        CourseDataGrid.ItemsSource = _courses;
    }

    private void Back(object? sender, RoutedEventArgs e)
    {
        MenuWindow mwin = new MenuWindow();
        mwin.Show();
        this.Close();
    }

    private void Add(object? sender, RoutedEventArgs e)
    {
        CourseAddWindow mwin = new CourseAddWindow();
        mwin.Show();
        this.Close();
    }

    private void Button_OnClick_Delete(object? sender, RoutedEventArgs e)
    {
        using (var conn = new MySqlConnection(_constring))
        {
            conn.Open();
            using (var cmd = new MySqlCommand("DELETE FROM Courses WHERE Course_ID LIKE " + IDTextBox.Text, conn))
            {
                cmd.ExecuteNonQuery();
                ShowTable(fulltable);
            }
            conn.Close();
        }
    } 
}
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPPrivalov.Students;

public partial class StudentsWindow : Window
{
    private string _constring =
        "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659989;UID=sql12659989;PASSWORD=nDB33bCp4Y;";
    
    private List<StudentClass> _students;
    private MySqlConnection _connection;

    private string fulltable =
        "SELECT Student_ID, Students.Name, Students.Surname, Students.LastName, Students.Birthday, Students.Phone_Number," +
        " Students.Email, Students.Language_Experience, Language_Experience.Language_Experience_Name, Students.Language_Level," +
        " Language_Levels.Level_Name FROM Students " +
        "JOIN Language_Experience on Students.Language_Experience = Language_Experience.Language_Experience_ID " +
        "JOIN Language_Levels on Students.Language_Level = Language_Levels.Language_Level_ID";
    
    public StudentsWindow()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _students = new List<StudentClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new StudentClass()
            {
                StudentID = reader.GetInt32("Student_ID"),
                StudentName = reader.GetString("Name"),
                StudentSurname = reader.GetString("Surname"),
                StudentLastName = reader.GetString("LastName"),
                StudentBirthday = reader.GetDateTime("Birthday"),
                StudentPhone = reader.GetString("Phone_Number"),
                StudentEmail = reader.GetString("Email"),
                StudentLangExp = reader.GetString("Language_Experience_Name"),
                StudentLangLvl = reader.GetString("Level_Name")
            };
            _students.Add(current);
        }
        _connection.Close();
        StudentsDataGrid.ItemsSource = _students;
    }
    
    private void Back(object? sender, RoutedEventArgs e)
    {
        MenuWindow mwin = new MenuWindow();
        mwin.Show();
        this.Close();
    }
}
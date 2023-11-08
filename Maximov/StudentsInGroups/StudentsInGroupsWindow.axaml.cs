using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using UPPrivalov.SGroups;

namespace UPPrivalov.StudentsInGroups;

public partial class StudentsInGroupsWindow : Window
{
    private string _constring =
        "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659989;UID=sql12659989;PASSWORD=nDB33bCp4Y;";
    
    private List<StudentInGroupClass> _studentsingroup;
    private MySqlConnection _connection;

    private string fulltable =
        "SELECT Student_In_Group_ID, Students_In_Groups.Student, Students.Name, Students_In_Groups.SGroup, SGroups.Group_Name FROM Students_In_Groups " +
        "JOIN Students on Students_In_Groups.Student = Students.Student_ID " +
        "JOIN SGroups on Students_In_Groups.SGroup = SGroups.Group_ID";
    
    public StudentsInGroupsWindow()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _studentsingroup = new List<StudentInGroupClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new StudentInGroupClass()
            {
                StudentInGroupID = reader.GetInt32("Student_In_Group_ID"),
                Student = reader.GetString("Name"),
                SGroup = reader.GetString("Group_Name")
            };
            _studentsingroup.Add(current);
        }
        _connection.Close();
        StudentsInGroupsDataGrid.ItemsSource = _studentsingroup;
    }
    
    private void Back(object? sender, RoutedEventArgs e)
    {
        MenuWindow mwin = new MenuWindow();
        mwin.Show();
        this.Close();
    }
    
    private void Add(object? sender, RoutedEventArgs e)
    {
        StudentAddToGroupWindow mwin = new StudentAddToGroupWindow();
        mwin.Show();
        this.Close();
    }
    private void Button_OnClick_Delete(object? sender, RoutedEventArgs e)
    {
        using (var conn = new MySqlConnection(_constring))
        {
            conn.Open();
            using (var cmd = new MySqlCommand("DELETE FROM Students_In_Groups WHERE Student_In_Group_ID LIKE " + IDTextBox.Text, conn))
            {
                cmd.ExecuteNonQuery();
                ShowTable(fulltable);
            }
            conn.Close();
        }
    } 
}
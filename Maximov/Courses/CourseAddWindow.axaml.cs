using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPPrivalov.Courses;

public partial class CourseAddWindow : Window
{
    private DB db = new DB();
    private string _constring =
        "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659989;UID=sql12659989;PASSWORD=nDB33bCp4Y;";
    
    private MySqlConnection _connection;
    public CourseAddWindow()
    {
        InitializeComponent();
        
        using (db.getConnection())
        {
            db.OpenConnection();
            
            var query = "SELECT Language_Study_Name FROM Language_Study";
            using (var command = new MySqlCommand(query, db.getConnection()))
            {
                using (var reader = command.ExecuteReader())
                {
                    //Iterate through the rows and add it to the combobox's items
                    while (reader.Read())
                    {
                        StudyLangComboBox.Items.Add(reader.GetString("Language_Study_Name"));    
                    }
                }    
            }
            
            db.CloseConnection();
        }
    }

    private void AddButton(object? sender, RoutedEventArgs e)
    {
        MySqlCommand command = new MySqlCommand("INSERT INTO sql12659989.Courses (Course_Name, Course_Description, Language_Study, Price)" +
                                                "VALUES (@name, @desc, @lang, @price)",db.getConnection());
        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = CourceNameBox.Text;
        command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = CourseDescriptionBox.Text;
        command.Parameters.Add("@lang", MySqlDbType.Int32).Value = StudyLangComboBox.SelectedIndex+1;
        command.Parameters.Add("@price", MySqlDbType.Double).Value = PriceBox.Text;
            
        db.OpenConnection();
        command.ExecuteNonQuery();
        db.CloseConnection();
        
        CourseWindow w = new CourseWindow();
        w.Show();
        this.Close();
    }
    private void Cancel(object? sender, RoutedEventArgs e)
    {
        MenuWindow win = new MenuWindow();
        win.Show();
        this.Close();
    }
}
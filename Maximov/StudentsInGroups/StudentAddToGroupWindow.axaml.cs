using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using UPPrivalov.SGroups;

namespace UPPrivalov.StudentsInGroups;

public partial class StudentAddToGroupWindow : Window
{
    private DB db = new DB();
    private string _constring =
        "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659989;UID=sql12659989;PASSWORD=nDB33bCp4Y;";
    
    private MySqlConnection _connection;
    public StudentAddToGroupWindow()
    {
        InitializeComponent();
        
        using (db.getConnection())
        {
            db.OpenConnection();
            
            var query = "SELECT Group_Name FROM SGroups";
            using (var command = new MySqlCommand(query, db.getConnection()))
            {
                using (var reader = command.ExecuteReader())
                {
                    //Iterate through the rows and add it to the combobox's items
                    while (reader.Read())
                    {
                        GroupingComboBox.Items.Add(reader.GetString("Group_Name"));    
                    }
                }    
            }
            
            db.CloseConnection();
        }
        
        using (db.getConnection())
        {
            db.OpenConnection();
            
            var query = "SELECT Name FROM Students";
            using (var command = new MySqlCommand(query, db.getConnection()))
            {
                using (var reader = command.ExecuteReader())
                {
                    //Iterate through the rows and add it to the combobox's items
                    while (reader.Read())
                    {
                        StudentGroupComboBox.Items.Add(reader.GetString("Name"));    
                    }
                }    
            }
            
            db.CloseConnection();
        }
    }

    private void AddButton(object? sender, RoutedEventArgs e)
    {
        MySqlCommand command = new MySqlCommand("INSERT INTO sql12659989.Students_In_Groups (Student, SGroup)" +
                                                "VALUES (@Student, @SGroup)",db.getConnection());
        command.Parameters.Add("@Student", MySqlDbType.Int32).Value = GroupingComboBox.SelectedIndex+1;
        command.Parameters.Add("@SGroup", MySqlDbType.Int32).Value = StudentGroupComboBox.SelectedIndex+1;
            
        db.OpenConnection();
        command.ExecuteNonQuery();
        db.CloseConnection();
        
        StudentsInGroupsWindow w = new StudentsInGroupsWindow();
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
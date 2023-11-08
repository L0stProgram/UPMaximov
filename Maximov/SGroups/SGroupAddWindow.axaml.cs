using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPPrivalov.SGroups;

public partial class SGroupAddWindow : Window
{
    private DB db = new DB();
    private string _constring =
        "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659989;UID=sql12659989;PASSWORD=nDB33bCp4Y;";
    
    private MySqlConnection _connection;
    public SGroupAddWindow()
    {
        InitializeComponent();
        
        using (db.getConnection())
        {
            db.OpenConnection();
            
            var query = "SELECT TeacherName FROM Teachers";
            using (var command = new MySqlCommand(query, db.getConnection()))
            {
                using (var reader = command.ExecuteReader())
                {
                    //Iterate through the rows and add it to the combobox's items
                    while (reader.Read())
                    {
                        PedagogueComboBox.Items.Add(reader.GetString("TeacherName"));    
                    }
                }    
            }
            
            db.CloseConnection();
        }
        
        using (db.getConnection())
        {
            db.OpenConnection();
            
            var query = "SELECT Course_Name FROM Courses";
            using (var command = new MySqlCommand(query, db.getConnection()))
            {
                using (var reader = command.ExecuteReader())
                {
                    //Iterate through the rows and add it to the combobox's items
                    while (reader.Read())
                    {
                        CourseGroupComboBox.Items.Add(reader.GetString("Course_Name"));    
                    }
                }    
            }
            
            db.CloseConnection();
        }
    }

    private void AddButton(object? sender, RoutedEventArgs e)
    {
        MySqlCommand command = new MySqlCommand("INSERT INTO sql12659989.SGroups (Group_Name, Pedagogue, Max_Students, Course)" +
                                                "VALUES (@groupName, @pedagogue, @maxStudents, @course)",db.getConnection());
        command.Parameters.Add("@groupName", MySqlDbType.VarChar).Value = GroupNameBox.Text;
        command.Parameters.Add("@pedagogue", MySqlDbType.Int32).Value = PedagogueComboBox.SelectedIndex+1;
        command.Parameters.Add("@maxStudents", MySqlDbType.Int32).Value = MaxStudBox.Text;
        command.Parameters.Add("@course", MySqlDbType.Int32).Value = CourseGroupComboBox.SelectedIndex+1;
            
        db.OpenConnection();
        command.ExecuteNonQuery();
        db.CloseConnection();
        
        SGroupsWindow w = new SGroupsWindow();
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
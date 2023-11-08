using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using UPPrivalov.SGroups;

namespace UPPrivalov.Teachers;

public partial class TeacherAddWindow : Window
{
    private DB db = new DB();
    private string _constring =
        "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659989;UID=sql12659989;PASSWORD=nDB33bCp4Y;";
    
    private MySqlConnection _connection;
    public TeacherAddWindow()
    {
        InitializeComponent();
    }

    private void AddButton(object? sender, RoutedEventArgs e)
    {
        MySqlCommand command = new MySqlCommand("INSERT INTO sql12659989.Teachers (TeacherName, Surname, LastName, Birthday, Phone_Number, Email)" +
                                                "VALUES (@teacherName, @teacherSurname, @teacherLastName, @teacherBirthday,@teacherPhone,@teacherEmail)",db.getConnection());
        command.Parameters.Add("@teacherName", MySqlDbType.VarChar).Value = TeacherNameTextBox.Text;
        command.Parameters.Add("@teacherSurname", MySqlDbType.VarChar).Value = TeacherSurnameTextBox.Text;
        command.Parameters.Add("@teacherLastName", MySqlDbType.VarChar).Value = TeacherLastNameTextBox.Text;
        command.Parameters.Add("@teacherBirthday", MySqlDbType.DateTime).Value = TeacherBirthdayPicker.SelectedDate;
        command.Parameters.Add("@teacherPhone", MySqlDbType.VarChar).Value = TeacherPhoneTextBox.Text;
        command.Parameters.Add("@teacherEmail", MySqlDbType.VarChar).Value = TeacherEmailTextBox.Text;
        
        db.OpenConnection();
        command.ExecuteNonQuery();
        db.CloseConnection();
        
        TeachersWindow w = new TeachersWindow();
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
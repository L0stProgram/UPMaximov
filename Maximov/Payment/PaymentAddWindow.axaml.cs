using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPPrivalov;

public partial class PaymentAddWindow : Window
{
    private DB db = new DB();
    private string _constring =
        "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659989;UID=sql12659989;PASSWORD=nDB33bCp4Y;";

    private MySqlConnection _connection;
    public PaymentAddWindow()
    {
        InitializeComponent();

        using (db.getConnection())
        {
            db.OpenConnection();

            var query = "SELECT Student_ID FROM Students";
            using (var command = new MySqlCommand(query, db.getConnection()))
            {
                using (var reader = command.ExecuteReader())
                {
                    //Iterate through the rows and add it to the combobox's items
                    while (reader.Read())
                    {
                        StudentComboBoxx.Items.Add(reader.GetString("Student_ID"));
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
                        CourseComboBoxx.Items.Add(reader.GetString("Course_Name"));
                    }
                }
            }

            db.CloseConnection();
        }

        using (db.getConnection())
        {
            db.OpenConnection();

            var query = "SELECT Payment_Type_Name FROM Payment_Types";
            using (var command = new MySqlCommand(query, db.getConnection()))
            {
                using (var reader = command.ExecuteReader())
                {
                    //Iterate through the rows and add it to the combobox's items
                    while (reader.Read())
                    {
                        PayTypeBoxx.Items.Add(reader.GetString("Payment_Type_Name"));
                    }
                }
            }

            db.CloseConnection();
        }
    }
    private void AddButton(object? sender, RoutedEventArgs e)
    {
        MySqlCommand command = new MySqlCommand("INSERT INTO sql12659989.Payments (Course, Student, Sum, Payment_Type)" +
                                                "VALUES (@Course, @Student, @Sum, @Payment_Type)",db.getConnection());
        command.Parameters.Add("@Course", MySqlDbType.VarChar).Value = CourseComboBoxx.SelectedIndex+1;
        command.Parameters.Add("@Student", MySqlDbType.VarChar).Value = StudentComboBoxx.SelectedIndex+1;
        command.Parameters.Add("@Sum", MySqlDbType.Int32).Value = SumBox.Text;
        command.Parameters.Add("@Payment_Type", MySqlDbType.Double).Value = PayTypeBoxx.SelectedIndex+1;

        db.OpenConnection();
        command.ExecuteNonQuery();
        db.CloseConnection();

        PaymentsWindow w = new PaymentsWindow();
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
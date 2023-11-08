using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPPrivalov;

public partial class PaymentsWindow : Window
{
    private string _constring =
        "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659989;UID=sql12659989;PASSWORD=nDB33bCp4Y;";
    
    private List<PaymentClass> _payments;
    private MySqlConnection _connection;
    private string fulltable = "SELECT Payment_ID, Payments.Course, Courses.Course_Name ,Payments.Student, Students.Name, Sum, Payments.Payment_Type, Payment_Types.Payment_Type_Name FROM Payments " +
                               "JOIN Payment_Types on Payments.Payment_Type = Payment_Types.Payment_Type_ID " +
                               "JOIN Students on Payments.Student = Students.Student_ID " +
                               "JOIN Courses on Payments.Course = Courses.Course_ID";
    public PaymentsWindow()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _payments = new List<PaymentClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new PaymentClass()
            {
                PaymentID = reader.GetInt32("Payment_ID"),
                PaymentCourseName = reader.GetString("Course_Name"),
                PaymentStudent = reader.GetString("Name"),
                PaymentCourseSum = reader.GetDouble("Sum"),
                Payment_type = reader.GetString("Payment_Type_Name")
            };
            _payments.Add(current);
        }
        _connection.Close();
        PaymentsDataGrid.ItemsSource = _payments;
    }

    private void Back(object? sender, RoutedEventArgs e)
    {
        MenuWindow mwin = new MenuWindow();
        mwin.Show();
        this.Close();
    }
    private void Add(object? sender, RoutedEventArgs e)
    {
        PaymentAddWindow mwin = new PaymentAddWindow();
        mwin.Show();
        this.Close();
    }
    private void Button_OnClick_Delete(object? sender, RoutedEventArgs e)
    {
        using (var conn = new MySqlConnection(_constring))
        {
            conn.Open();
            using (var cmd = new MySqlCommand("DELETE FROM Payments WHERE Payment_ID LIKE " + IDTextBox.Text, conn))
            {
                cmd.ExecuteNonQuery();
                ShowTable(fulltable);
            }
            conn.Close();
        }
    } 

}
using System;
using System.Data;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;

namespace UPPrivalov;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Login(object? sender, RoutedEventArgs e)
    {
        String UserLogin = LoginBox.Text;
        String UserPassword = PasswordBox.Text;
        
        DB db = new DB();
        
        DataTable table = new DataTable();
        
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        
        MySqlCommand command = new MySqlCommand("SELECT * FROM Students WHERE Login = @uL AND Password = @uP",db.getConnection());
        
        command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = UserLogin;
        command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = UserPassword;

        adapter.SelectCommand = command;
        adapter.Fill(table);

        if (table.Rows.Count > 0)
        {
            Console.WriteLine("Yes");
            MenuWindow mwin = new MenuWindow();
            mwin.Show();
            this.Close();
        }
        else 
            Console.WriteLine("No");
    }

    private void Register(object? sender, RoutedEventArgs e)
    {
        RegisterWindow regwin = new RegisterWindow();
        regwin.Show();
        this.Close();
    }
}
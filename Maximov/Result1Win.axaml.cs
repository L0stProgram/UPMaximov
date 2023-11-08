using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPPrivalov;

public partial class Result1Win : Window
{
    public Result1Win()
    {
        InitializeComponent();
        CountBlock.Text = getResults().ToString();
    }

    private int getResults()
    {
        DB db = new DB();
        using (db.getConnection())
        {
            db.OpenConnection();
            using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM Payments", db.getConnection()))
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                db.CloseConnection();
                return count;
            }
            
        }
    }

    private void Back(object? sender, RoutedEventArgs e)
    {
        MenuWindow mwin = new MenuWindow();
        mwin.Show();
        this.Close();
    }
}
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using Tmds.DBus.Protocol;

namespace UPPrivalov;

public partial class RegisterWindow : Window
{
    DB db = new DB();
    public RegisterWindow()
    {
        InitializeComponent();

        using (db.getConnection())
        {
            db.OpenConnection();
            
            var query = "SELECT Level_Name FROM Language_Levels";
            using (var command = new MySqlCommand(query, db.getConnection()))
            {
                using (var reader = command.ExecuteReader())
                {
                    //Iterate through the rows and add it to the combobox's items
                    while (reader.Read())
                    {
                        LevelsComboBox.Items.Add(reader.GetString("Level_Name"));    
                    }
                }    
            }
            
            db.CloseConnection();
        }
        
        using (db.getConnection())
        {
            db.OpenConnection();
            
            var query = "SELECT Language_Experience_Name FROM Language_Experience";
            using (var command = new MySqlCommand(query, db.getConnection()))
            {
                using (var reader = command.ExecuteReader())
                {
                    //Iterate through the rows and add it to the combobox's items
                    while (reader.Read())
                    {
                        ExpComboBox.Items.Add(reader.GetString("Language_Experience_Name"));    
                    }
                }    
            }
            
            db.CloseConnection();
        }
    }

    private void Register(object? sender, RoutedEventArgs e)
    {
        if (NameBox.Text == null || EmailBox.Text == null || LoginBox.Text == null || PasswordBox.Text == null || PhoneBox.Text == null || LastNameBox.Text == null || LevelsComboBox.SelectedIndex == null || ExpComboBox.SelectedIndex == null || SurnameBox.Text == null || BirthdayPicker.SelectedDate == null)
        {
            ErrorBlock.Text = "Заполните все данные";
        }
        
        else
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO sql12659989.Students (Name, Surname, LastName, Birthday, Phone_Number, Email, Language_Experience,Language_Level, Login, Password)" +
                                                    "VALUES (@name, @surname, @lastname, @birthday, @phone, @email, @exp, @level, @login, @password)",db.getConnection());
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = NameBox.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = SurnameBox.Text;
            command.Parameters.Add("@lastname", MySqlDbType.VarChar).Value = LastNameBox.Text;
            command.Parameters.Add("@birthday", MySqlDbType.Date).Value = BirthdayPicker.SelectedDate;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = PhoneBox.Text;
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = EmailBox.Text;
            command.Parameters.Add("@exp", MySqlDbType.Int32).Value = ExpComboBox.SelectedIndex+1;
            command.Parameters.Add("@level", MySqlDbType.Int32).Value = LevelsComboBox.SelectedIndex+1;
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = LoginBox.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = PasswordBox.Text;
            
            db.OpenConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Fail");
            }
            
            db.CloseConnection();
            this.Close();
        }
    }

    private void Cancel(object? sender, RoutedEventArgs e)
    {
        MainWindow win = new MainWindow();
        win.Show();
        this.Close();
    }
}
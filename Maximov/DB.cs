using MySql.Data.MySqlClient;
using Tmds.DBus.Protocol;

namespace UPPrivalov;

public class DB
{
    private MySqlConnection connection = new MySqlConnection("SERVER=sql12.freesqldatabase.com;DATABASE=sql12659989;UID=sql12659989;PASSWORD=nDB33bCp4Y;");

    public void OpenConnection()
    {
        if (connection.State == System.Data.ConnectionState.Closed)
        {
            connection.Open();
        }
    }
    
    public void CloseConnection()
    {
        if (connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
        }
    }

    public MySqlConnection getConnection()
    {
        return connection;
    }
}
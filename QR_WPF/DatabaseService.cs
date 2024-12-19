using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QR_WPF
{
    public class DatabaseService
    {
        private const string dbPath = "./database.sqlite3";

        private static SQLiteConnection GetConnection()
        {
            SQLiteConnection conn = new SQLiteConnection($"Data Source={dbPath};Version=3");
            return conn;
        }
        
        public static bool AddUser(string username, string password)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    string SQLQuery = "INSERT INTO users (login, password_hash) VALUES (@login, @password_hash)";
                    using (var command = new SQLiteCommand(SQLQuery, connection))
                    {
                        SHA256 sha256 = SHA256.Create();
                        StringBuilder hash_hex = new StringBuilder();
                        byte[] hash_bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                        foreach (byte b in hash_bytes)
                        {
                            hash_hex.Append(b.ToString("x2"));
                        }

                        command.Parameters.AddWithValue("@login", username);
                        command.Parameters.AddWithValue("@password_hash", hash_hex.ToString());
                        command.ExecuteNonQuery();

                    }
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка: {e.ToString()}", "Ошибка регистрации");
                return false;
            }
            
        }

        public static (bool, string) Authenticate(string login, string password)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string sqlQuery = "SELECT login FROM users WHERE login=@login AND password_hash=@password_hash;";
                    SQLiteCommand command = new SQLiteCommand(sqlQuery, conn);
                    
                    SHA256 sha256 = SHA256.Create();
                    byte[] hash_bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    StringBuilder hash_hex = new StringBuilder();

                    foreach (byte b in hash_bytes)
                    {
                        hash_hex.Append(b.ToString("x2"));
                    }
                    
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password_hash", hash_hex.ToString());

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string returnLogin = reader["login"].ToString();
                            return (true, returnLogin);
                        }
                        else
                        {
                            MessageBox.Show("Неверное имя пользователя или пароль.", "Ошибка входа");
                            return (false, "");
                        }
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка {e.ToString()}", "Ошибка входа");
                return (false, "");
            }
        }
    }
    
}

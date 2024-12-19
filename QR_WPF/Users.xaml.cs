using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QR_WPF
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Page
    {
        public Users()
        {
            InitializeComponent();
            LoadDataFromDatabase();
        }
        private void LoadDataFromDatabase()
        {
            // Путь к вашей базе данных SQLite
            string connectionString = "Data Source=./database.sqlite3;Version=3;";

            // Список для хранения пользователей
            List<User> users = new List<User>();

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // SQL-запрос для получения логинов
                    string query = "SELECT login FROM users";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(new User
                                {
                                    Login = reader["login"].ToString()
                                });
                            }
                        }
                    }
                }

                // Привязка данных к ListView
                MyListView.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при работе с базой данных: {ex.Message}");
            }
        }
        private void Back(object sender, RoutedEventArgs e) 
        {
            NavigationService.GoBack();
        }
    }

    public class User
    {
        public string Login { get; set; }
    }
    

}

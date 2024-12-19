using System;
using System.Collections.Generic;
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
using System.Data.SQLite;
using System.Diagnostics;

namespace QR_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string db_path = "./database.sqlite3";
        public MainWindow()
        {
            InitDB();
            InitializeComponent();

            MainFrame.Content = new Login();
        }

        private void InitDB() {
            if (!System.IO.File.Exists(db_path))
            {
                SQLiteConnection.CreateFile(db_path);

            }
            using (var db = new SQLiteConnection($"Data Source={db_path};Version=3"))
            {
                db.Open();
                string sqlInitQuery = @"
                    CREATE TABLE IF NOT EXISTS users (
                    login TEXT NOT NULL PRIMARY KEY,
                    password_hash TEXT NOT NULL);
                ";
                SQLiteCommand command = new SQLiteCommand(sqlInitQuery, db);
                command.ExecuteNonQuery();
            }
        }
        
    }
}

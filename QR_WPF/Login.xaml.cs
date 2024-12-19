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

namespace QR_WPF
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void NavigateToSignup(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Signup());
        }

        private void SubmitLogin(object sender, RoutedEventArgs e)
        {
            string login = login_field.Text;
            string password = password_field.Password;

            var status = DatabaseService.Authenticate(login, password);

            if (status.Item1) {
                
                MessageBox.Show($"Вход выполнен успешно под пользователем {status.Item2}");
                NavigationService.Navigate(new Index());
            }
            else
            {
                return;
            }
        }
    }
}

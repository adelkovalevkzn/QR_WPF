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
    /// Логика взаимодействия для Signup.xaml
    /// </summary>
    public partial class Signup : Page
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void NavigateToLogin(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login());
        }

        private void SubmitSignup(object sender, RoutedEventArgs e) 
        {
            string login = login_field.Text;
            string password = password_field.Password;
            string password2 = password2_field.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(password2)) {
                MessageBox.Show("Заполнены не все значения", "Ошибка регистрации");
                return;
            }
            if (password != password2) {
                MessageBox.Show("Пароли не совпадают", "Ошибка регистрации");
                return;
            }

            bool status = DatabaseService.AddUser(login, password);
            if (status)
            {
                MessageBox.Show($"Вы успешно зарегистировались под логином {login}", "Успех");
                NavigationService.Navigate(new Login());

            }
            else {
                return;
            }

        }
    }

    
}

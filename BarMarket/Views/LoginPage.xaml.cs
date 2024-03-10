using BarMarket.DB;
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

namespace BarMarket.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void SignUpNavigate(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPass.Password;

            if (password.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать не менее 6 символов");
                return;
            }

            try
            {
                var currentUser = ConnectData.db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
                if (currentUser != null)
                {
                    User1.userId = currentUser.ID;
                    NavigationService.Navigate(new HomePage());
                }
                else
                {
                    MessageBox.Show("Данного пользователя нет в базе");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении запроса к базе данных: {ex.Message}");
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text == "E-Mail")
            {
                txtLogin.Text = "";
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                txtLogin.Text = "E-Mail";
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPass.Visibility = Visibility.Visible;
            txtPass.Focus();
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPass.Password))
            {
                txtPass.Visibility = Visibility.Collapsed;
            }
        }

    }
}

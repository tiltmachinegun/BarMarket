using BarMarket.DB;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BarMarket.Views
{
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPass.Password;
            string confirmPassword = txtConfirmPass.Password;

            if (password != confirmPassword)
            {
                ErrorMessage.Text = "Пароли не совпадают";
                return;
            }

            if (password.Length < 6)
            {
                ErrorMessage.Text = "Пароль должен содержать не менее 6 символов";
                return;
            }

            try
            {
                var existingUser = ConnectData.db.Users.FirstOrDefault(u => u.Login == login);
                if (existingUser != null)
                {
                    ErrorMessage.Text = "Пользователь с таким логином уже существует";
                    return;
                }

                var newUser = new User
                {
                    Login = login,
                    Password = password,
                    Role = "User" // По умолчанию роль "User"
                };

                ConnectData.db.Users.Add(newUser);
                ConnectData.db.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно!");
                NavigationService.Navigate(new LoginPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}");
            }
        }

        private void LoginNavigate(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}
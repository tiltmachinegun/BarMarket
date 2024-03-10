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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void SignInNavigate(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
        private void Register(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            string login = txtLogin.Text.Trim();
            string password = txtPass.Password;

            if (password.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать не менее 6 символов");
                return;
            }

            try
            {
                var existingUser = ConnectData.db.Users.FirstOrDefault(u => u.Login == login);
                if (existingUser != null)
                {
                    MessageBox.Show("Пользователь с таким именем уже зарегистрирован");
                    return;
                }

                User userObj = new User()
                {
                    ID = rnd.Next(1, 10000),
                    Login = login,
                    Password = password,
                    Role = "",
                };
                ConnectData.db.Users.Add(userObj);
                ConnectData.db.SaveChanges();
                

                MessageBox.Show("Регистрация прошла успешно");
                User1.userId = userObj.ID;


                NavigationService.Navigate(new HomePage()); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении запроса к базе данных: {ex.Message}");
            }
        }
    }
}

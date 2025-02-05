using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BarMarket.DB;

namespace BarMarket.Views
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginTextBox.Text;
            var password = PasswordBox.Password;

            var currentUser = ConnectData.db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

            if (currentUser != null)
            {
                // Сохраняем текущего пользователя
                UserSession.CurrentUser = currentUser;

                // Открываем главное окно
                var mainWindow = new MainWindow();
                mainWindow.Show();

                // Закрываем текущее окно
                var window = Window.GetWindow(this);
                window?.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход на страницу регистрации
            NavigationService.Navigate(new RegisterPage());
        }
    }
}
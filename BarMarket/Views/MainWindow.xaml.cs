using System.Windows;
using System.Windows.Controls;

namespace BarMarket.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Загружаем главную страницу по умолчанию
            NavigateToHomePage(null, null);
        }

        private void NavigateToHomePage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HomePage());
        }

        private void NavigateToCatalogPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CatalogPage());
        }

        private void NavigateToCartPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CartPage());
        }

        private void NavigateToProfilePage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProfilePage());
        }

        private void NavigateToNotificationsPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new NotificationsPage());
        }
    }
}
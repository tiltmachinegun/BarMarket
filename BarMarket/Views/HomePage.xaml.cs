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
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private void NavigateListBeverages(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListBeveragesPage());
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
        private void GoToWriteOffPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WriteOffPage());
        }
        private void HistoryGo(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new History());
        }
    }
}

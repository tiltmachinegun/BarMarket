using BarMarket.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Логика взаимодействия для ListBeveragesPage.xaml
    /// </summary>
    public partial class History : Page
    {
        public ObservableCollection<Supply> Supplies { get; set; }

        public History()
        {
            InitializeComponent();
            Supplies = new ObservableCollection<Supply>(ConnectData.db.Supplies.ToList());
            LViewTours.ItemsSource = Supplies;
        }



        public void RemoveBlock(object sender, RoutedEventArgs e)
        {
            var selectedBlocks = LViewTours.SelectedItems.Cast<Supply>().ToList();

            if (selectedBlocks.Count == 0)
            {
                MessageBox.Show("Вы не выбрали товары");
                return;
            }

            string confirmationMessage;
            if (selectedBlocks.Count == 1)
            {
                confirmationMessage = "Вы действительно хотите удалить эту строку из истории?";
            }
            else
            {
                confirmationMessage = "Вы действительно хотите удалить эти строки из истории?";
            }

            if (MessageBox.Show(confirmationMessage, "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var block in selectedBlocks)
                {
                    ConnectData.db.Supplies.Remove(block);
                    Supplies.Remove(block); 
                }

                ConnectData.db.SaveChanges();
                MessageBox.Show("Удалено");
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            Supplies = new ObservableCollection<Supply>(ConnectData.db.Supplies.ToList());
            LViewTours.ItemsSource = Supplies;
        }

        public void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HomePage());
        }

    }
}

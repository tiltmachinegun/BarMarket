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
    public partial class ListBeveragesPage : Page
    {
        public ObservableCollection<Beverage> Beverages { get; set; }

        public ListBeveragesPage()
        {
            InitializeComponent();
            Beverages = new ObservableCollection<Beverage>(ConnectData.db.Beverages.ToList());
            LViewTours.ItemsSource = Beverages;
        }

        public void AddBlock(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddBeveragesPage());
        }

        public void RemoveBlock(object sender, RoutedEventArgs e)
        {
            
                var selectedBlocks = LViewTours.SelectedItems.Cast<Beverage>().ToList();
                if (selectedBlocks.Count == 0)
                {
                    MessageBox.Show("Вы не выбрали товары");
                    return;
                }

                string confirmationMessage;
                if (selectedBlocks.Count == 1)
                {
                    confirmationMessage = "Вы действительно хотите удалить этот товар со склада?";
                }
                else
                {
                    confirmationMessage = "Вы действительно хотите удалить эти товары со склада?";
                }
                var username = GetUsernameById(User1.userId);
               
                if (MessageBox.Show(confirmationMessage, "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var block in selectedBlocks)
                    {
                        var supply = new Supply
                        {
                            Action = "Удаление позиции",
                            Beverage_Name = block.Name,
                            Beverage_ID = block.Id,
                            Beverage_Type = block.Type,
                            Beverage_Percent = block.Percent,
                            Beverage_Creator = block.Creator,
                            Supply_ID = null,
                            Supply_Date = DateTime.Now.Date,
                            Supplier = block.Supplier,
                            Quantity = block.Quantity,
                            Beverage_Volume = block.Volume,
                            Beverage_Price = block.Price,
                            Activated = null,
                            Username = username
                        };


                        ConnectData.db.Supplies.Add(supply);
                        ConnectData.db.SaveChanges();

                        ConnectData.db.Beverages.Remove(block);
                        ConnectData.db.SaveChanges();

                        Beverages.Remove(block); 
                        
                    }
                MessageBox.Show("Удалено");

            }
        }

        
        private string GetUsernameById(int userId)
        {
            
            {
                var user = ConnectData.db.Users.FirstOrDefault(u => u.ID == userId);
                if (user != null)
                {
                    return user.Login;
                }
                else
                {
                    
                    return string.Empty;
                }
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            Beverages = new ObservableCollection<Beverage>(ConnectData.db.Beverages.ToList());
            LViewTours.ItemsSource = Beverages;
        }

        public void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HomePage());
        }

    }
}

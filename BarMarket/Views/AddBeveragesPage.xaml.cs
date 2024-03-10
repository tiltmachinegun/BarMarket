using BarMarket.DB;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace BarMarket.Views
{
    /// <summary>
    /// Логика взаимодействия для AddBeveragesWindow.xaml
    /// </summary>
    public partial class AddBeveragesPage : Page
    {
        public AddBeveragesPage()
        {
            InitializeComponent();
        }

        private void AddBeverage(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = TbName.Text;
                string type = TbType.Text;
                string creator = TbCreator.Text;
                decimal volume = Convert.ToDecimal(TbVolume.Text);
                decimal percent = Convert.ToDecimal(TbPercent.Text);
                decimal price = Convert.ToDecimal(TbPrice.Text);
                int quantity = Convert.ToInt32(TbQuantity.Text);
                var username = GetUsernameById(User1.userId);
                DateTime dateAdded = DateTime.Now;
                string supplier = TbSupplier.Text;
                Random random = new Random();

                Beverage existingBeverage = ConnectData.db.Beverages.FirstOrDefault(b => b.Name == name && b.Type == type);
                Beverage newBeverage = null;

                if (existingBeverage != null)
                {
                    existingBeverage.Quantity += quantity;
                    existingBeverage.TimeStamp = dateAdded;
                }
                else
                {
                    newBeverage = new Beverage
                    {
                        Name = name,
                        Type = type,
                        Creator = creator,
                        Volume = volume,
                        Percent = percent,
                        Price = price,
                        Quantity = quantity,
                        Supplier = supplier,
                        TimeStamp = dateAdded
                    };
                    ConnectData.db.Beverages.Add(newBeverage);
                    ConnectData.db.SaveChanges();
                }

                int beverageId = newBeverage != null ? newBeverage.Id : existingBeverage.Id; 

                Supply newSupply = new Supply
                {
                    Username = username,
                    Action = "Поставка",
                    Beverage_Name = name,
                    Beverage_Type = type,
                    Beverage_ID = beverageId,
                    Beverage_Volume = volume,
                    Beverage_Creator = creator,
                    Beverage_Percent = percent,
                    Quantity = quantity,
                    Supply_Date = dateAdded,
                    Supplier = supplier,
                    Activated = true
                };

                ConnectData.db.Supplies.Add(newSupply);
                ConnectData.db.SaveChanges();

                MessageBox.Show("Товар успешно добавлен.");
                TbName.Text = string.Empty;
                TbType.Text = string.Empty;
                TbCreator.Text = string.Empty;
                TbVolume.Text = string.Empty;
                TbPercent.Text = string.Empty;
                TbPrice.Text = string.Empty;
                TbQuantity.Text = string.Empty;
                TbSupplier.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
                TbName.Text = string.Empty;
                TbType.Text = string.Empty;
                TbCreator.Text = string.Empty;
                TbVolume.Text = string.Empty;
                TbPercent.Text = string.Empty;
                TbPrice.Text = string.Empty;
                TbQuantity.Text = string.Empty;
                TbSupplier.Text = string.Empty;
            }
        }


        private void Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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


    }
}
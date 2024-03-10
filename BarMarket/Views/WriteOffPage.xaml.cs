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
    /// Логика взаимодействия для WriteOffPage.xaml
    /// </summary>
    public partial class WriteOffPage : Page
    {
        public WriteOffPage()
        {
            InitializeComponent();
        }
        private void WriteOff(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получите значения полей ввода
                int beverageId = Convert.ToInt32(TbBeverageId.Text);
                int quantity = Convert.ToInt32(TbQuantity.Text);
                DateTime dateWrittenOff = DateTime.Now;
                var username = GetUsernameById(User1.userId);
                // Проверьте, существует ли товар с указанным ID в таблице Beverages
                Beverage existingBeverage = ConnectData.db.Beverages.FirstOrDefault(b => b.Id == beverageId);

                if (existingBeverage != null)
                {
                    // Проверьте, достаточно ли товара для списания
                    if (existingBeverage.Quantity >= quantity)
                    {
                        // Создайте сообщение с выбором подтверждения
                        string message = $"Вы точно уверены, что хотите списать {existingBeverage.Name} в количестве {quantity} штук?";
                        MessageBoxResult result = MessageBox.Show(message, "Подтверждение списания", MessageBoxButton.YesNo);

                        if (result == MessageBoxResult.Yes)
                        {
                            // Уменьшите количество товара
                            existingBeverage.Quantity -= quantity;
                            

                            // Создайте новую запись лога списания
                            Supply newWriteOff = new Supply
                            {
                                Beverage_Name = existingBeverage.Name,
                                Beverage_Type = existingBeverage.Type,
                                Beverage_Percent = existingBeverage.Percent,
                                Beverage_Creator = existingBeverage.Creator,
                                Supply_ID = null,
                                Supplier = existingBeverage.Supplier,
                                Activated = null,
                                Username = username,
                                Beverage_ID = beverageId,
                                Quantity = quantity,
                                Supply_Date = dateWrittenOff,
                                Action = "Cписание"
                            };

                            // Добавьте запись лога списания в таблицу Supplies
                            ConnectData.db.Supplies.Add(newWriteOff);

                            // Сохраните изменения в базе данных
                            ConnectData.db.SaveChanges();

                            MessageBox.Show("Списание успешно выполнено.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Недостаточно товара для списания указанного количества.");
                    }
                }
                else
                {
                    MessageBox.Show("Товар с указанным ID не найден.");
                }

                // Очистите поля ввода после списания
                TbBeverageId.Text = string.Empty;
                TbQuantity.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
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
        private void Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

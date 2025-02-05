using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BarMarket.DB;

namespace BarMarket.Views
{
    public partial class CartPage : Page
    {
        public CartPage()
        {
            InitializeComponent();
            LoadCartItems();
            CalculateTotalAmount();
        }

        // Загрузка товаров в корзине
        private void LoadCartItems()
        {
            if (UserSession.CurrentUser == null)
            {
                MessageBox.Show("Авторизуйтесь для просмотра корзины.");
                return;
            }

            try
            {
                CartItemsList.ItemsSource = ConnectData.db.Carts
                    .Include(c => c.ProductId)
                    .Where(c => c.UserId == UserSession.CurrentUser.ID)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки корзины: {ex.Message}");
            }
        }

        // Обновление количества товара
        private void UpdateQuantity(Cart item, int delta)
        {
            item.Quantity += delta;

            if (item.Quantity <= 0)
            {
                ConnectData.db.Carts.Remove(item);
            }

            ConnectData.db.SaveChanges();
            LoadCartItems();
            CalculateTotalAmount();
        }

        // Увеличение количества
        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            var item = (Cart)((Button)sender).DataContext;
            UpdateQuantity(item, 1);
        }

        // Уменьшение количества
        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            var item = (Cart)((Button)sender).DataContext;
            UpdateQuantity(item, -1);
        }

        // Удаление товара из корзины
        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            var item = (Cart)((Button)sender).DataContext;
            ConnectData.db.Carts.Remove(item);
            ConnectData.db.SaveChanges();
            LoadCartItems();
            CalculateTotalAmount();
        }

        // Расчет общей суммы
        private void CalculateTotalAmount()
        {
            if (CartItemsList.ItemsSource == null) return;

            decimal totalAmount = 0;
            foreach (var item in (List<Cart>)CartItemsList.ItemsSource)
            {
                totalAmount += item.Product.Price * item.Quantity;
            }

            TotalAmount = totalAmount;
        }

        // Свойство для общей суммы
        public decimal TotalAmount
        {
            get { return (decimal)GetValue(TotalAmountProperty); }
            set { SetValue(TotalAmountProperty, value); }
        }

        public static readonly DependencyProperty TotalAmountProperty =
            DependencyProperty.Register("TotalAmount", typeof(decimal), typeof(CartPage), new PropertyMetadata(0m));

        // Оформление заказа
        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (UserSession.CurrentUser == null)
            {
                MessageBox.Show("Авторизуйтесь для оформления заказа.");
                return;
            }

            if (CartItemsList.ItemsSource == null || !((List<Cart>)CartItemsList.ItemsSource).Any())
            {
                MessageBox.Show("Корзина пуста.");
                return;
            }

            try
            {
                // Создание заказа
                var order = new Order
                {
                    UserId = UserSession.CurrentUser.ID,
                    OrderDate = DateTime.Now,
                    StatusId = 1, // Оформлен
                    DeliveryType = DeliveryRadio.IsChecked == true ? "Доставка" : "Самовывоз",
                    DeliveryAddress = AddressTextBox.Text,
                    TotalAmount = TotalAmount
                };

                ConnectData.db.Orders.Add(order);
                ConnectData.db.SaveChanges();

                // Добавление товаров в заказ
                foreach (var cartItem in (List<Cart>)CartItemsList.ItemsSource)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.Product.Price
                    };

                    ConnectData.db.OrderItems.Add(orderItem);
                }

                // Очистка корзины
                ConnectData.db.Carts.RemoveRange((List<Cart>)CartItemsList.ItemsSource);
                ConnectData.db.SaveChanges();

                MessageBox.Show("Заказ успешно оформлен!");
                NavigationService.Navigate(new CatalogPage()); // Переход в каталог
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка оформления заказа: {ex.Message}");
            }
        }
    }
}
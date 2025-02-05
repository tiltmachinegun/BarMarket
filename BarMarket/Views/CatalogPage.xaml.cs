using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BarMarket.DB;

namespace BarMarket.Views
{
    public partial class CatalogPage : Page
    {
        private List<Product> _allProducts = new List<Product>(); // Инициализация пустым списком

        public CatalogPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Проверка контекста базы данных
                if (ConnectData.db == null)
                {
                    MessageBox.Show("Ошибка подключения к базе данных.");
                    return;
                }

                // Загрузка товаров
                _allProducts = ConnectData.db.Products?.ToList() ?? new List<Product>();
                ProductsList.ItemsSource = _allProducts;

                // Загрузка категорий
                var categories = ConnectData.db.Categories?.ToList() ?? new List<Category>();
                CategoryFilter.ItemsSource = categories;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void ApplyFilters()
        {
            try
            {
                if (_allProducts == null || !_allProducts.Any())
                {
                    ProductsList.ItemsSource = null;
                    return;
                }

                var filteredProducts = _allProducts.AsQueryable();

                // Фильтр по поиску
                if (!string.IsNullOrEmpty(SearchTextBox.Text))
                {
                    filteredProducts = filteredProducts.Where(p => p.Name.Contains(SearchTextBox.Text));
                }

                // Фильтр по категории
                if (CategoryFilter.SelectedItem != null)
                {
                    var selectedCategory = (Category)CategoryFilter.SelectedItem;
                    filteredProducts = filteredProducts.Where(p => p.CategoryId == selectedCategory.Id);
                }

                // Фильтр по цене
                decimal maxPrice = Convert.ToDecimal(PriceSlider.Value);
                filteredProducts = filteredProducts.Where(p => p.Price <= maxPrice);

                // Фильтр по наличию
                if (InStockFilter.IsChecked == true)
                {
                    filteredProducts = filteredProducts.Where(p => p.Quantity > 0);
                }

                ProductsList.ItemsSource = filteredProducts.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка фильтрации: {ex.Message}");
            }
        }

        // Обработчики событий
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e) => ApplyFilters();
        private void CategoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyFilters();
        private void PriceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => ApplyFilters();
        private void InStockFilter_Checked(object sender, RoutedEventArgs e) => ApplyFilters();
        private void InStockFilter_Unchecked(object sender, RoutedEventArgs e) => ApplyFilters();

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserSession.CurrentUser == null)
                {
                    MessageBox.Show("Авторизуйтесь для добавления в корзину!");
                    return;
                }

                var selectedProduct = (Product)((Button)sender).DataContext;

                var cartItem = new Cart
                {
                    UserId = UserSession.CurrentUser.ID,
                    ProductId = selectedProduct.Id,
                    Quantity = 1,
                    ProductName = selectedProduct.Name
                };

                ConnectData.db.Carts.Add(cartItem);
                ConnectData.db.SaveChanges();

                MessageBox.Show("Товар добавлен в корзину!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
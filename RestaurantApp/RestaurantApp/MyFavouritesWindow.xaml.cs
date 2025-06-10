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
using System.Windows.Shapes;
using Serilog;

namespace RestaurantApp
{
    /// <summary>
    /// Interaction logic for MyFavouritesWindow.xaml
    /// </summary>
    public partial class MyFavouritesWindow : Window
    {
        private string filePath = "restaurant_data.txt";
        private RestaurantCollection restaurantCollection;
        private List<RestaurantDisplay> restaurantDisplayList = new List<RestaurantDisplay>();
        public MyFavouritesWindow()
        {
            InitializeComponent();

            restaurantCollection = new RestaurantCollection();
            restaurantCollection.LoadFromFile(filePath);

            DisplayLikedRestaurants();


        }

        private void BackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            restaurantCollection.SaveToFile(filePath);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        public void DisplayLikedRestaurants()
        {
            WarpPanelRestaurants.Children.Clear();
            restaurantDisplayList.Clear();
            foreach (Restaurant restaurant in restaurantCollection.RestaurantList)
            {
                if (restaurant.IsLiked)
                {
                    restaurantDisplayList.Add(new RestaurantDisplay(restaurant.Name, restaurant.KindOfFood, restaurant.Address, restaurant.Rating, restaurant.Link, restaurant.IsLiked)
                    {
                        Comment = restaurant.Comment,
                    });
                    Log.Debug($"{restaurant.Name} | {restaurant.KindOfFood} | {restaurant.Address} | {restaurant.Rating} | {restaurant.Link} added to display list");
                    WarpPanelRestaurants.Children.Add(restaurantDisplayList.Last());
                }
            }
        }

        private void ComboboxSearchCritics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboboxSearchCritics.SelectedIndex)
            {
                case 0:
                    restaurantCollection.SortByNameAZ();
                    break;
                case 1:
                    restaurantCollection.SortByNameZA();
                    break;
                case 2:
                    restaurantCollection.SortByRating();
                    break;
                case 3:
                    restaurantCollection.SortByKindOfFood();
                    break;
            }
            DisplayLikedRestaurants();
        }

        private void TextboxSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = TextboxSearchBar.Text.ToLower();
            WarpPanelRestaurants.Children.Clear();
            restaurantDisplayList.Clear();
            foreach (Restaurant restaurant in restaurantCollection.RestaurantList)
            {
                if (restaurant.IsLiked && (
                    restaurant.Name.ToLower().Contains(searchText) ||
                    restaurant.KindOfFood.ToLower().Contains(searchText) ||
                    restaurant.Address.ToLower().Contains(searchText)))
                {
                    RestaurantDisplay display = new RestaurantDisplay(restaurant.Name, restaurant.KindOfFood, restaurant.Address, restaurant.Rating, restaurant.Link, restaurant.IsLiked)
                    {
                        Comment = restaurant.Comment,
                    };
                    restaurantDisplayList.Add(display);
                    WarpPanelRestaurants.Children.Add(display);
                }
            }
        }


        private void MyFavouritesWindowBackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}

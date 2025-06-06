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
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        private string filePath = "restaurant_data.txt";
        private List<RestaurantDisplay> restaurantDisplayList = new List<RestaurantDisplay>();
        private RestaurantCollection restaurantCollection;

        public SearchWindow()
        {
            InitializeComponent();

            restaurantCollection = new RestaurantCollection();
            restaurantCollection.LoadFromFile(filePath);

            DisplayRestaurants();
        }

        private void BackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            Log.Information("Back to Main Window");
            int index = 0;
            foreach (RestaurantDisplay restaurantDisplay in restaurantDisplayList)
            {
                restaurantCollection.RestaurantList[index].IsLiked = restaurantDisplay.IsLiked;
                Log.Debug($"{restaurantDisplay.Name} {restaurantDisplay.IsLiked}");
                Log.Debug($"{restaurantCollection.RestaurantList[index].Name} {restaurantCollection.RestaurantList[index].IsLiked}");
                index++;
            }
            restaurantCollection.SaveToFile(filePath);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
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
            DisplayRestaurants();
        }

        public void DisplayRestaurants()
        {
            WarpPanelRestaurants.Children.Clear();
            restaurantDisplayList.Clear();
            foreach (Restaurant restaurant in restaurantCollection.RestaurantList)
            {
                restaurantDisplayList.Add(new RestaurantDisplay(restaurant.Name, restaurant.KindOfFood, restaurant.Address, restaurant.Rating, restaurant.Link, restaurant.IsLiked)
                {
                    Comment = restaurant.Comment,
                    NameOfImmage = restaurant.NameOfImmage
                });
                Log.Debug($"{restaurant.Name} | {restaurant.KindOfFood} | {restaurant.Address} | {restaurant.Rating} | {restaurant.Link} added to display list");
                WarpPanelRestaurants.Children.Add(restaurantDisplayList.Last());
            }
        }

        private void TextboxSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

            string searchText = TextboxSearchBar.Text.ToLower();
            WarpPanelRestaurants.Children.Clear();
            restaurantDisplayList.Clear();
            foreach (Restaurant restaurant in restaurantCollection.RestaurantList)
            {
                if (restaurant.Name.ToLower().Contains(searchText) ||
                    restaurant.KindOfFood.ToLower().Contains(searchText) ||
                    restaurant.Address.ToLower().Contains(searchText))
                {
                    RestaurantDisplay display = new RestaurantDisplay(restaurant.Name, restaurant.KindOfFood, restaurant.Address, restaurant.Rating, restaurant.Link, restaurant.IsLiked)
                    {
                        Comment = restaurant.Comment,
                        NameOfImmage = restaurant.NameOfImmage
                    };
                    restaurantDisplayList.Add(display);
                    WarpPanelRestaurants.Children.Add(display);
                }
            }
        }
    }
}

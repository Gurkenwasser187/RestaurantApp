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
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.DevTools.V135.FileSystem;

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
        ChromeOptions options = new ChromeOptions();


        public SearchWindow()
        {
            InitializeComponent();

            RemoveDuplicatesFromFile();

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
                    };
                    restaurantDisplayList.Add(display);
                    WarpPanelRestaurants.Children.Add(display);
                }
            }
        }

        private void ButtonReloadImages_Click(object sender, RoutedEventArgs e)
        {
            foreach (Restaurant restaurant in restaurantCollection.RestaurantList)
            {
                try
                {
                    SaveScreenshotToFile(restaurant.Link, restaurant.Name);
                }
                catch
                {
                    Log.Warning($"Failed to save screenshot for {restaurant.Name} at {restaurant.Link}");
                }
            }
        }

        private void SaveScreenshotToFile(string linkToWebside, string restaurantName)
        {
            if (System.IO.File.Exists($"Restaurant_Imgs/{restaurantName}.png"))
            {
                using (IWebDriver driver = new ChromeDriver(options))
                {
                    driver.Navigate().GoToUrl(linkToWebside);
                    System.Threading.Thread.Sleep(2000);

                    Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                    string path = $"Restaurant_Imgs/{restaurantName}.png";
                    screenshot.SaveAsFile(path);

                    Log.Debug($"Screenshot of {restaurantName} saved to: {path}");
                }
            }
            else
            {
                Log.Debug($"Image for {restaurantName} already exists, skipping screenshot save.");
            }
        }

        public void RemoveDuplicatesFromFile()
        {
            if (!System.IO.File.Exists(filePath))
            {
                Log.Warning($"File {filePath} does not exist.");
                return;
            }

            try
            {
                string json = System.IO.File.ReadAllText(filePath);
                var restaurants = System.Text.Json.JsonSerializer.Deserialize<List<Restaurant>>(json);

                if (restaurants == null)
                {
                    Log.Warning("No restaurants found in the file.");
                    return;
                }

                var uniqueRestaurants = restaurants
                    .GroupBy(r => r.Name, StringComparer.OrdinalIgnoreCase)
                    .Select(g => g.First())
                    .ToList();

                int duplicatesCount = restaurants.Count - uniqueRestaurants.Count;

                string newJson = System.Text.Json.JsonSerializer.Serialize(uniqueRestaurants, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(filePath, newJson);

                if (duplicatesCount > 0)
                    Log.Information($"Removed {duplicatesCount} duplicate restaurants from the file.");
                else
                    Log.Information("No duplicate restaurants found in the file.");
            }
            catch (Exception ex)
            {
                Log.Error($"Error processing file: {ex.Message}");
            }
        }
    }
}

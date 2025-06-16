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
    /// Interaction logic for Top3Restaurants.xaml
    /// </summary>
    public partial class Top3Restaurants : Window
    {

        private RestaurantCollection _restaurantCollection;

        public Top3Restaurants(RestaurantCollection restaurants)
        {
            InitializeComponent();

            _restaurantCollection = new RestaurantCollection();
            _restaurantCollection.LoadFromFile("restaurant_data.json");
            
            ShowTop3Restaurants();
        }


       

        private void Top3RestaurantsBackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ShowTop3Restaurants()
        {
            if (_restaurantCollection.RestaurantList == null || !_restaurantCollection.RestaurantList.Any()) // Prüft , ob die Restaurantliste leer oder null ist
            {
                Log.Warning("No restaurants available to display.");
                MessageBox.Show("Keine Restaurants verfügbar.");
                return;

            }
           
            var top3Restaurants = _restaurantCollection.RestaurantList
                .OrderByDescending(r  => r.Rating)  // Nach Rating absteigend sortieren
                .Take(3)                             // Die ersten drei Restaurants nehmen
                .ToList();                            // In eine Liste umwandeln
            Log.Information("Top 3 Restaurants selected.");

            PanelTop3.Children.Clear();    // Löscht alte Anzeigeelemente im Panel, damit keine Duplikate entstehen

            foreach (var restaurant in top3Restaurants)
            {
                Log.Information($"Displaying restaurant: {restaurant.Name}, Rating: {restaurant.Rating}");

                var displayControl = new RestaurantDisplay(restaurant.Name, restaurant.KindOfFood,restaurant.Address,restaurant.Rating,restaurant.Link,restaurant.IsLiked)
                {
                    DataContext = restaurant    // Verbindet das Anzeigeelement mit den Infos des Restaurants 
                };

                
                PanelTop3.Children.Add(displayControl);  // Fügt das Anzeigeelement in das Panel ein
            }
        }
    }

    
}

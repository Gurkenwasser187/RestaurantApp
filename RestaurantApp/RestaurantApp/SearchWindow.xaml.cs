﻿using System;
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

namespace RestaurantApp
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        private string filePath = "restaurant_data.txt";
        private List<RestaurantDisplay> restaurantDisplayList = new List<RestaurantDisplay>();

        public SearchWindow()
        {
            InitializeComponent();

            RestaurantCollection restaurantCollection = new RestaurantCollection();
            restaurantCollection.LoadFromFile(filePath);


            // Alle Reastaurants anzeigen

            foreach (Restaurant restaurant in restaurantCollection.RestaurantList)
            {
                restaurantDisplayList.Add(new RestaurantDisplay()
                {
                    Name = restaurant.Name,
                    KindOfFood = restaurant.KindOfFood,
                    Address = restaurant.Address,
                    Rating = restaurant.Rating,
                    Link = restaurant.Link,
                    Comment = restaurant.Comment,
                    NameOfImmage = restaurant.NameOfImmage
                });
            }

        }

        private void BackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}

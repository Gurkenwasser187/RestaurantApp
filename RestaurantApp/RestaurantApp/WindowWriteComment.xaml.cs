using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for WindowWriteComment.xaml
    /// </summary>
    public partial class WindowWriteComment : Window
    {
        string CurrentRestaurantName;

        private readonly string FilePath = "restaurant_data.json";

        RestaurantCollection restaurantCollection = new RestaurantCollection();


        public WindowWriteComment(string name)
        {
            InitializeComponent();
            restaurantCollection.LoadFromFile(FilePath);
            CurrentRestaurantName = name;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        

        private void ButtonPost_Click(object sender, RoutedEventArgs e)
        {
            foreach(Restaurant restaurant in restaurantCollection.RestaurantList)
            {
                if(restaurant.Name == CurrentRestaurantName)
                {
                    restaurant.Comment = TextBoxComment.Text;
                    
                    break;
                }
            }

            restaurantCollection.SaveToFile(FilePath);

            this.Close();
        }

        private void CommentWrite_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxComment.Text = "";
        }
    }
}

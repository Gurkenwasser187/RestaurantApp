using OpenQA.Selenium.DevTools;
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
    /// Interaction logic for MyCommentsWindow.xaml
    /// </summary>
    public partial class MyCommentsWindow : Window
    {
        private RestaurantCollection restaurantCollection;


        public MyCommentsWindow()
        {
            InitializeComponent();
            restaurantCollection = new RestaurantCollection();
            restaurantCollection.LoadFromFile("restaurant_data.json");
            LoadComments();
            


        }


        private void LoadComments()
        {
            CommentsPanel.Children.Clear();

            
            foreach(Restaurant restaurant in restaurantCollection.RestaurantList)
            {

                if (!string.IsNullOrEmpty(restaurant.Comment))
                {

                    MyCommentsDisplaycontroll myCommentsDisplaycontroll = new MyCommentsDisplaycontroll(restaurantCollection, restaurant.Name, restaurant.Comment);

                    myCommentsDisplaycontroll.CommentDelete += ReloadComments;


                    CommentsPanel.Children.Add(myCommentsDisplaycontroll);

                    Serilog.Log.Debug($"Comment for {restaurant.Name} was loaded successfully");

                }
            }
        }

        private void ReloadComments(object? sender, EventArgs e)
        {
            LoadComments();
        }

        private void CommentsBackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void MyCommentsComboboxSearchCritics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (MyCommentsComboboxSearchCritics.SelectedIndex){
                case 0:
                    restaurantCollection.SortByNameAZ();
                    break;
                case 1:
                    restaurantCollection.SortByNameZA();
                    break;
            }
            LoadComments();
        }
    } 
}

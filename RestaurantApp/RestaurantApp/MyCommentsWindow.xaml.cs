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

namespace RestaurantApp
{
    /// <summary>
    /// Interaction logic for MyCommentsWindow.xaml
    /// </summary>
    public partial class MyCommentsWindow : Window
    {

        

        public MyCommentsWindow()
        {
            InitializeComponent();
            LoadComments();


        }


        private void LoadComments()
        {
            CommentsPanel.Children.Clear();

            RestaurantCollection restaurantCollection = new RestaurantCollection();
            restaurantCollection.LoadFromFile("restaurant_data.json");
            foreach(Restaurant restaurant in restaurantCollection.RestaurantList)
            {

                if (!string.IsNullOrEmpty(restaurant.Comment))
                {

                    MyCommentsDisplaycontroll myCommentsDisplaycontroll = new MyCommentsDisplaycontroll(restaurantCollection,restaurant.Name,restaurant.Comment);

                    myCommentsDisplaycontroll.CommentDelete += ReloadComments;

                    CommentsPanel.Children.Add(myCommentsDisplaycontroll);
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
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestaurantApp
{
    /// <summary>
    /// Interaction logic for MyCommentsDisplaycontroll.xaml
    /// </summary>
    public partial class MyCommentsDisplaycontroll : UserControl
    {
        RestaurantCollection restaurantCollection;

        public string DisplayName { get; set; }
        public string Comment { get; set; }

        public event EventHandler CommentDelete;


        public MyCommentsDisplaycontroll(RestaurantCollection restaurantCollection_,string displayname, string comment)
        {
            InitializeComponent();
            DisplayName = displayname;
            Comment = comment;
            restaurantCollection = restaurantCollection_;

            MyCommentsTextBlock.Text = comment;
            MyCommentsDisplayControllLabelName.Content = DisplayName;

        }

        
        private void WriteCommentButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            foreach (Restaurant restaurant in restaurantCollection.RestaurantList)
            {
                if (restaurant.Name == DisplayName)
                {
                    restaurant.Comment = null;

                    break;
                }
            }

            restaurantCollection.SaveToFile("restaurant_data.json");

            CommentDelete?.Invoke(this, new EventArgs());
        }
    }
}

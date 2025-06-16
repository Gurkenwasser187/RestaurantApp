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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Serilog;
using System.Runtime.ConstrainedExecution;

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

        // Ereignis, was geschehen wird, wenn ein Kommentar gelöscht wird
        public event EventHandler CommentDelete;


        public MyCommentsDisplaycontroll(RestaurantCollection restaurantCollection_,string displayname, string comment)
        {
            InitializeComponent();
            // Übergabewerte in die Eigenschaften speichern
            DisplayName = displayname;
            Comment = comment;
            restaurantCollection = restaurantCollection_;

            //Kommentierter Restaurant wird in der GUI angezeigt
            MyCommentsTextBlock.Text = comment;

            //Restaurant Name wird in der GUI angezeigt
            MyCommentsDisplayControllLabelName.Content = DisplayName;

            Serilog.Log.Debug($"MyCommentsDisplayControll created for restaurant {DisplayName} with comment: {Comment}");
            
        }

        
        private void WriteCommentButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            foreach (Restaurant restaurant in restaurantCollection.RestaurantList) // Geht alle Restaurants durch
            {
                if (restaurant.Name == DisplayName) // Prüft, ob der Name des aktuellen Restaurants dem angezeigten Namen entspricht 
                {
                    Serilog.Log.Debug($"Deleting comment for restaurant {DisplayName}");

                    restaurant.Comment = null; // Kommentar wird Gelöscht,indem es auf null gesetzt wird
                }
            }

            restaurantCollection.SaveToFile("restaurant_data.json"); //Speichert die aktualisierte Liste in der Datei
            Serilog.Log.Debug($"Comment for {DisplayName} got successfully deleted and changes were saved");
           

            CommentDelete?.Invoke(this, new EventArgs()); // Meldet, dass der Kommentar gelöscht wurde

        }
    }
}

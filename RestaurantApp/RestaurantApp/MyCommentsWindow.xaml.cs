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
            restaurantCollection = new RestaurantCollection(); // Neue Sammlung wird erstellt
            restaurantCollection.LoadFromFile("restaurant_data.json"); // Kommentare aus diesem File Laden
            LoadComments(); // Kommentare anzeigen

        }

        private void LoadComments() 
        {
            CommentsPanel.Children.Clear(); // Löscht die vorherigen Kommentare

            foreach(Restaurant restaurant in restaurantCollection.RestaurantList)  // Geht alle Restaurants durch
            {
                if (!string.IsNullOrEmpty(restaurant.Comment)) // Nur Restaurants mit einem Kommentar anzeigen
                {
                    // Neues Anzeige-Element für den Kommentar erstellen
                    MyCommentsDisplaycontroll myCommentsDisplaycontroll = new MyCommentsDisplaycontroll(restaurantCollection, restaurant.Name, restaurant.Comment);

                    // Event verbinden: Wenn Kommentar gelöscht wird, Liste neu laden
                    myCommentsDisplaycontroll.CommentDelete += ReloadComments;

                    // Kommentar wird zu GUI hinzugefügt
                    CommentsPanel.Children.Add(myCommentsDisplaycontroll);
                     
                    Serilog.Log.Debug($"Comment for {restaurant.Name} was loaded successfully");

                }
            }
        }

        private void ReloadComments(object? sender, EventArgs e)
        {

            LoadComments(); // Aktualisiert Liste, wenn ein Kommentar gelöscht wurde
        }

        private void CommentsBackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void MyCommentsComboboxSearchCritics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Sortierungen der Restaurants:

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

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
       
        string CurrentRestaurantName;  // Der Name des restaurants, für das gerade ein Kommentar geschrieben wird

        private readonly string FilePath = "restaurant_data.json";

        RestaurantCollection restaurantCollection = new RestaurantCollection();

        private const int maxWord = 50;

        public WindowWriteComment(string name)
        {
            InitializeComponent();

            restaurantCollection.LoadFromFile(FilePath); // Ladet bestehende Restaurantdaten aus der Datei

            CurrentRestaurantName = name; // Speichert den Namen des Restaurants, für das der Kommentar geschrieben wird
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        
        private void ButtonPost_Click(object sender, RoutedEventArgs e)
        {
            foreach(Restaurant restaurant in restaurantCollection.RestaurantList)
            {
                if(restaurant.Name == CurrentRestaurantName) // Sucht das Restaurant, zu dem gerade der Kommentar geschrieben wird
                {
                    restaurant.Comment = TextBoxComment.Text; // Speichert den eingegebenen Kommentar beim entsprechenden Restaurant

                }
                Log.Debug($"Comment for restaurant {restaurant.Name} was posted successfully: {restaurant.Comment}");
            }
            restaurantCollection.SaveToFile(FilePath); // Speichert die komplette Sammlung wieder in die Datei mit den neuen Kommentars


            if (string.IsNullOrWhiteSpace(TextBoxComment.Text) || TextBoxComment.Text == "Schreibe etwas...") //Prüft ob Eingabe Leer ist
            {
               
                MessageBox.Show("Bitte schreiben sie etwas");
                return; // Bricht die Methode ab, damit das Fenster offen bleibt
            }
            this.Close();
        }

        private void CommentWrite_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxComment.Text = ""; // Der Platzhaler wird gelöscht
        }

        private void TextBoxComment_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Teilt den text in Wörter auf indem nach Leerzeichen, Zeilenumbrüchen usw. getrennt wird
            string[] words = TextBoxComment.Text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries); 

            if (words.Length > maxWord) 
            {
                MessageBox.Show($"Man darf maximal {maxWord} Wörter schreiben.");
                TextBoxComment.Text = null;

            }

        }
    }
}

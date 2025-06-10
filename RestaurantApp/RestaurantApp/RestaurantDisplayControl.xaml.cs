using Serilog;
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
    /// Interaction logic for RestaurantDisplay.xaml
    /// </summary>
    public partial class RestaurantDisplay : UserControl
    {
        public string Name { get; set; }
        public string KindOfFood { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        public string Link { get; set; }
        public string? Comment { get; set; }
        public Boolean IsLiked { get; set; }

        public RestaurantDisplay(string name, string kindOfFood, string address, double rating, string link, bool isliked)
        {
            Name = name;
            KindOfFood = kindOfFood;
            Address = address;
            Rating = rating;
            Link = link;
            IsLiked = isliked;

            InitializeComponent();
            Log.Debug($"{Name} | {KindOfFood} | {Address} | {Rating} | {Link} Display configured");
            LabelName.Content = Name;
            LabelTypeOfFood.Content = KindOfFood;
            LabelLocation.Content = Address;
            LabelLink.Content = Link;
            StarLabel.Content = new string('★', (int)Rating) + new string('☆', 5 - (int)Rating);

            ImageSource RestaurantImage = new BitmapImage();

            string imagePath = $"Restaurant_Imgs/{Name}.png";
            if (System.IO.File.Exists(imagePath))
            {
                Log.Debug($"Image found: {imagePath} | {name}");
                ImageRestaurant.Source = new ImageSourceConverter().ConvertFromString(imagePath) as ImageSource;
            }
            else
            {
                ImageRestaurant.Source = new ImageSourceConverter().ConvertFromString("Restaurant_Imgs/NoImageFuond.png") as ImageSource;
                Log.Warning($"Image not found: {imagePath} | {name}");
            }


            if (IsLiked)
            {
                ButtonLike.Background = Brushes.LightGreen;
            }
        }

        private void ButtonCopyLink_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Link))
            {
                Clipboard.SetText(Link);
            }
        }

        private void ButtonLike_Click(object sender, RoutedEventArgs e)
        {
            IsLiked = !IsLiked;
            if (IsLiked)
            {
                ButtonLike.Background = Brushes.LightGreen;
            }
            else
            {
                ButtonLike.Background = Brushes.Transparent;
            }
        }
    }
}

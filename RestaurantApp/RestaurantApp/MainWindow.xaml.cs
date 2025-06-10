using Serilog;
using System.Text;
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
using System.IO;

namespace RestaurantApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow searchWindow = new SearchWindow();
            searchWindow.Show();
            this.Close();
        }

        private void ButtonLikes_Click(object sender, RoutedEventArgs e)
        {
            MyFavouritesWindow myFavouritesWindow = new MyFavouritesWindow();
            myFavouritesWindow.Show();
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonComments_Click(object sender, RoutedEventArgs e)
        {
            MyCommentsWindow myCommentsWindow = new MyCommentsWindow();
            myCommentsWindow.Show();
            this.Close();
        }
    }
}
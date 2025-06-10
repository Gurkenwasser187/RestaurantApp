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


        private void LoadComments(){
            foreach (var comment in WindowWriteComment.CommentsList)
            {
               
                ListBoxComments.Items.Add(comment);

            }
        }

        private void CommentsBackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}

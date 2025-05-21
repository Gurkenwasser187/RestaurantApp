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
        public string? NameOfImmage { get; set; }

        public RestaurantDisplay()
        {
            InitializeComponent();
        }
    }
}

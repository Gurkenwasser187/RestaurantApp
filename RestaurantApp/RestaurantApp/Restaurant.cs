using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestaurantApp
{
    public class Restaurant
    {
        public string Name { get; set; }
        public string KindOfFood { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        public string Link { get; set; }
        public string? Comment { get; set; }
        public string? NameOfImmage { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestaurantApp
{
    class Restaurant
    {
        public string Name { get; set; }
        private string _KindOfFood;

        public string KindOfFood
        {
            get { return _KindOfFood; }
            set { _KindOfFood = value; }
        }

        public string Address { get; set; }
        private int _rating;

        public int Rating
        {
            get { return _rating; }
            set 
            {
                if (!(value < 0 || value > 5))
                {
                    _rating = value;
                }
            }
        }

        public string Link { get; set; }
        public string Comment { get; set; }
        public string NameOfImmage { get; set; }

        public Restaurant(string name, string kindOfFood, string address, string link, string nameOfImmage)
        {
            Name = name;
            KindOfFood = kindOfFood;
            Address = address;
            Link = link;
            NameOfImmage = nameOfImmage;
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static Restaurant Deserialize(string json)
        {
            return JsonSerializer.Deserialize<Restaurant>(json);
        }
    }
}

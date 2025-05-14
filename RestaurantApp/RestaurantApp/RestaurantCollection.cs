using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp
{
    class RestaurantCollection
    {
        public List<Restaurant> RestaurantList { get; set; }

        public RestaurantCollection()
        {
            RestaurantList = new List<Restaurant>();
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            RestaurantList.Add(restaurant);
        }

        public void SavetoFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Restaurant restaurant in RestaurantList)
                {
                    writer.WriteLine(restaurant.Serialize());
                }
            }
        }

        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Restaurant restaurant = Restaurant.Deserialize(line);
                        RestaurantList.Add(restaurant);
                    }
                }
            }
        }
    }
}

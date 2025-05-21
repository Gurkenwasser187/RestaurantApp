using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
                    // writer.WriteLine(restaurant.Serialize());
                }
            }
        }

        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);

                RestaurantList = JsonSerializer.Deserialize<List<Restaurant>>(jsonString);
            }
        }

        public void SortByName()
        {
            RestaurantList = RestaurantList.OrderBy(r => r.Name).ToList();
        }

        public void SortByKindOfFood()
        {
            RestaurantList = RestaurantList.OrderBy(r => r.KindOfFood).ToList();
        }

        public void SortByDistence()
        {
            //ToDo: Implement distance sorting
        }

        public void SortByRating()
        {
            RestaurantList = RestaurantList.OrderBy(r => r.Rating).ToList();
            RestaurantList.Reverse();
        }
    }
}

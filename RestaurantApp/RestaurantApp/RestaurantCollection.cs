using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Serilog;

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
            //Save to file in JSON format
            
        }

        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);

                RestaurantList = JsonSerializer.Deserialize<List<Restaurant>>(jsonString) ?? new List<Restaurant>();
                Log.Information($"Loaded {RestaurantList.Count} restaurants from {filePath}");
            }
            else
            {
                RestaurantList = new List<Restaurant>();
                Log.Warning($"File {filePath} does not exist");
            }
        }

        public void SaveToFile(string filePath)
        {
            foreach (var restaurant in RestaurantList)
            {
                Log.Debug($"{restaurant.IsLiked}");
            }
            string jsonString = JsonSerializer.Serialize(RestaurantList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
            
            Log.Information($"Saved {RestaurantList.Count} restaurants to {filePath}");
        }

        public void SortByNameAZ()
        {
            RestaurantList = RestaurantList.OrderBy(r => r.Name).ToList();
            Log.Information("Sorted restaurants by name A-Z");
        }

        public void SortByNameZA()
        {
            RestaurantList = RestaurantList.OrderByDescending(r => r.Name).ToList();
            Log.Information("Sorted restaurants by name Z-A");
        }

        public void SortByKindOfFood()
        {
            RestaurantList = RestaurantList.OrderBy(r => r.KindOfFood).ToList();
            Log.Information("Sorted restaurants by kind of food");
        }

        public void SortByDistence()
        {
            //ToDo: Implement distance sorting
        }

        public void SortByRating()
        {
            RestaurantList = RestaurantList.OrderBy(r => r.Rating).ToList();
            RestaurantList.Reverse();
            Log.Information("Sorted restaurants by rating");
        }
    }
}

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
        private static readonly string FilePath = "comments.json";

        public static List<string> CommentsList = new List<string>();


        public static void AddComment(string comment)
        {
            if (!string.IsNullOrWhiteSpace(comment))
            {
                CommentsList.Add(comment);
                SaveCommentsToFile();
            }
        }


        public static void SaveCommentsToFile()
        {
            try
            {
                var json = JsonSerializer.Serialize(CommentsList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Log.Warning($"Could not save comments | error: {ex.Message}");
            }
        }

        public static void LoadCommentsFromFile()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var json = File.ReadAllText(FilePath);
                    CommentsList = JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
                }
            }
            catch (Exception ex)
            {
                Log.Warning($"Could not load comments | error: {ex.Message}");
            }
        }

        public WindowWriteComment()
        {
            InitializeComponent();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

        private void ButtonPost_Click(object sender, RoutedEventArgs e)
        {
            string comment = TextBoxComment.Text;

            AddComment(comment);

            TextBoxComment.Clear();
            this.Close();
        }
    }
}

using System.Configuration;
using System.Data;
using System.Windows;
using Serilog;

namespace RestaurantApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(
                    "Restaurant.log",
                    rollOnFileSizeLimit: true,
                    fileSizeLimitBytes: 10485760
                )
                .CreateLogger();
        }
    }
}

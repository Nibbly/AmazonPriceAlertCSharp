using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonPriceAlert
{
    public class FileManager
    {
        public string ProductDataPath { get; }

        public FileManager()
        {
            ProductDataPath = GetWatchlistPath();
            Directory.CreateDirectory(Path.Combine(ProductDataPath, "Data"));
            ProductDataPath = Path.Combine(ProductDataPath, "Data", "Watchlist.json");
        }


        private string GetWatchlistPath()
        {
            string destinationDir = Environment.CurrentDirectory;

            for (var i = 0; i < 3; i++)
            {
                destinationDir = Directory.GetParent(destinationDir).ToString();
            }

            return destinationDir;
        }
    }
}

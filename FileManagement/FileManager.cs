using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AmazonPriceAlert
{
    public class FileManager
    {
        public string ProductDataDirectory { get; private set; }
        public string ImagesDirectory { get; private set; }

        public FileManager()
        {
            InitializeRessourceStructure();
        }

        /// <summary>
        /// Creates folders for the watchlist and the images
        /// structure: Ressources -> Data -> Watchlist.json
        /// Ressources -> Images
        /// </summary>
        private void InitializeRessourceStructure()
        {
            // go up 3 levels from bin
            string nthDirectory = GetNthParentDirecoty(3);

            // Create Data folder with watchlist.json in it
            ProductDataDirectory = Directory.CreateDirectory(Path.Combine(nthDirectory, "Ressources", "Data")).FullName;
            string filePath = Path.Combine(ProductDataDirectory, "Watchlist.json");

            if (!File.Exists(filePath))
            {
                var jsonFile = File.Create(filePath);
                jsonFile.Close();
            }

            ProductDataDirectory = filePath;

            //Create Images folder
            ImagesDirectory = Directory.CreateDirectory(Path.Combine(nthDirectory, "Ressources", "Images")).FullName;
        }

        private string GetNthParentDirecoty(int nthParent)
        {
            string destinationDir = Environment.CurrentDirectory;

            for (var i = 0; i < nthParent; i++)
            {
                destinationDir = Directory.GetParent(destinationDir).ToString();
            }

            return destinationDir;
        }

        public void SaveImage(string url, string filename)
        {
            //Check if image with same guid already exists
            var existingFiles = Directory.GetFiles(ImagesDirectory, "*.jpeg").Select(Path.GetFileName);

            foreach (var existingFile in existingFiles)
            {
                if (existingFile.Contains(filename))
                    return;
            }

            string imgName = Path.Combine(ImagesDirectory, filename);
            imgName += ".jpeg";

            using (var imgClient = new WebClient())
            {
                imgClient.DownloadFile(url, imgName);
            }
        }
    }
}

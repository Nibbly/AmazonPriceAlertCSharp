using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonPriceAlert
{
    public class Serializer
    {
        private string _filePathProducts;

        public Serializer()
        {
            FileManager manager = new FileManager();
            _filePathProducts = manager.ProductDataPath;
        }

        public void SerializeData(List<Product> productList)
        {
            var json = JsonConvert.SerializeObject(productList);
            File.WriteAllText(_filePathProducts, json);
        }

        public List<Product>GetSerializedData()
        {
            string json = "";
            List<Product> cList;

            using (StreamReader reader = new StreamReader(_filePathProducts))
            {
                json = reader.ReadToEnd();
                cList = JsonConvert.DeserializeObject<List<Product>>(json);

                return cList;
            }
        }
    }
}


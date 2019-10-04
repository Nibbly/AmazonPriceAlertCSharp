using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonPriceAlert
{
    public class Product
    {
        public string Id { get; set; }
        public string ProductUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public double InitialPrice { get; set; }
        public double TargetPrice { get; set; }

        public Product()
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonPriceAlert
{
    public class Product
    {
        public string Url { get; set; }
        public string Name { get; private set; }
        public double PriceWhenAdded { get; private set; }
        public double TargetPrice { get; set; }

        public Product(string url)
        {
            Url = url;
            HtmlHelper helper = new HtmlHelper(url);
            Name = helper.GetStringValueOfSpan("productTitle");
            PriceWhenAdded = helper.GetDoubleValueOfSpan("priceblock_ourprice");
        }
    }
}

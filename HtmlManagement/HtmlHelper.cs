using HtmlAgilityPack;
using HtmlManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AmazonPriceAlert
{
    public class HtmlHelper
    {
        private string _html;
        private string _url;
        public string Name { get; private set; }
        public double InitialPrice { get; private set; }
        public string ImageUrl { get; private set; }


        public HtmlHelper(string url)
        {
            _url = url;
            WebClient client = new WebClient();
            _html = client.DownloadString(url);

            Name = GetStringValueOfSpan(AmazonIds.PRODUCT_NAME_ID);
            ImageUrl = GetImageUrl(AmazonIds.PRODUCT_IMAGE_ID);
            InitialPrice = GetDoubleValueOfSpan(AmazonIds.PRODUCT_PRICE_ID);
        }

        private string GetStringValueOfSpan(string nodeId)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(_html);
            var node = doc.DocumentNode.SelectSingleNode($"//span[@id='{nodeId}']");

            if (node != null)
            {
                string innerText = innerText = Regex.Replace(node.InnerText, "\n", "");
                innerText = Regex.Replace(innerText, " ", "");
                return innerText;
            }

            return "NONE";
        }

        private double GetDoubleValueOfSpan(string nodeId)
        {
            string value = GetStringValueOfSpan(nodeId);
            string doublePattern = "(\\d)+,(\\d){2}";
            var doubleAsString = Regex.Match(value, doublePattern).ToString();
            var price = Convert.ToDouble(doubleAsString);

            return price;
        }

        private string GetImageUrl(string nodeId)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(_html);
            var node = doc.DocumentNode.SelectSingleNode($"//img[@id='{nodeId}']");
            var imageUrl = node.Attributes.Where(p => p.Name == AmazonIds.PRODUCT_IMAGE_URL_ATTRIBUTE).First().Value;

            return imageUrl;
        }
    }
}

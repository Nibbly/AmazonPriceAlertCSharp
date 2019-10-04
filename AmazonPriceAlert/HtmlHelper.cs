using HtmlAgilityPack;
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

        public HtmlHelper(string url)
        {
            WebClient client = new WebClient();
            _html = client.DownloadString(url);
        }

        public string GetStringValueOfSpan(string spanId)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(_html);
            var node = doc.DocumentNode.SelectSingleNode($"//span[@id='{spanId}']");

            if (node != null)
            {
                string innerText = innerText = Regex.Replace(node.InnerText, "\n", "");
                innerText = Regex.Replace(innerText, " ", "");
                return innerText;
            }

            return "NONE";
        }

        public double GetDoubleValueOfSpan(string spanId)
        {
            string value = GetStringValueOfSpan(spanId);
            string doublePattern = "(\\d)+,(\\d){2}";
            var doubleAsString = Regex.Match(value, doublePattern).ToString();
            var price = Convert.ToDouble(doubleAsString);

            return price;
        }
    }
}

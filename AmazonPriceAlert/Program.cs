using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AmazonPriceAlert
{
    class Program
    {
        const string url1 = "https://smile.amazon.de/GOURMETmaxx-02914-Frischhaltedosen-Gefrierschrank-Sp%C3%BClmaschine/dp/B00JXHA2OA/ref=sr_1_1?__mk_de_DE=%C3%85M%C3%85%C5%BD%C3%95%C3%91&keywords=tupperdose&qid=1569951659&s=musical-instruments&sr=8-1";
        const string url2 = "https://smile.amazon.de/gp/product/B009S1YVM0/ref=crt_ewc_img_oth_1?ie=UTF8&smid=A3JWKAKR8XB7XF&th=1&psc=1";


        static void Main(string[] args)
        {
            Product p1 = new Product(url1);
            Product p2 = new Product(url2);

            List <Product> products = new List<Product>();
            products.Add(p1);
            products.Add(p2);

            Serializer s = new Serializer();
            s.SerializeData(products);

            List<Product> newList = new List<Product>();
            newList = s.GetSerializedData();
        }
    }
}

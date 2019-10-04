using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        const string url3 = "https://smile.amazon.de/FIFA-20-Standard-PlayStation-4/dp/B07S55NLMW?pf_rd_p=f7c4c02b-950c-4430-910c-6d7703ed2215&pd_rd_wg=7lFKV&pf_rd_r=8KAG0WJZ9TQN7DGWYPD4&ref_=pd_gw_newr&pd_rd_w=4Skdj&pd_rd_r=57f55e57-06b8-4c29-a73b-0795f429ec2f";


        static void Main(string[] args)
        {
            //SetDummyData();

            Serializer s = new Serializer();
            var products = s.GetSerializedData();
            FileManager manager = new FileManager();

            foreach (var p in products)
            {
                manager.SaveImage(p.ImageUrl, p.Id.ToString());
            }

            string firstProd = products.ElementAt(1).Id;

            Process.Start((Path.Combine(manager.ImagesDirectory, firstProd) + ".jpeg"));

        }


        private static void SetDummyData()
        {
            List<Product> products = new List<Product>();

            HtmlHelper helper = new HtmlHelper(url1);
            Product p1 = new Product() { Id = new Guid().ToString(), ProductUrl = url1, Name = helper.Name, InitialPrice = helper.InitialPrice, ImageUrl = helper.ImageUrl };
            products.Add(p1);

            helper = new HtmlHelper(url2);
            Product p2 = new Product() { Id = new Guid().ToString(), ProductUrl = url2, Name = helper.Name, InitialPrice = helper.InitialPrice, ImageUrl = helper.ImageUrl };
            products.Add(p2);

            helper = new HtmlHelper(url3);
            Product p3 = new Product() { Id = new Guid().ToString(), ProductUrl = url3, Name = helper.Name, InitialPrice = helper.InitialPrice, ImageUrl = helper.ImageUrl };
            products.Add(p3);

            Serializer s = new Serializer();
            s.SerializeData(products);
        }

        private static void PrintCollection<T>(List<T> pList)
        {
            foreach (var p in pList)
            {
                var product = p as Product;
                Console.WriteLine("Name: \n\t" + product.Name);
                Console.WriteLine("Init Price: \n\t" + product.InitialPrice);
                Console.WriteLine("Img: \n\t" + product.ImageUrl);
            }
        }
    }
}

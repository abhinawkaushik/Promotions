using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using PromotionsEngine.Models;
using PromotionsEngine;
using PromotionsEngine.Rules;
using System.IO;

namespace Promotions
{
    class Program
    {
        static DataSource dataSource;
        static PromotionManager promotionManager;
        static void Main(string[] args)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string _url = projectDirectory + "\\" + ConfigurationManager.AppSettings["connectionString"];
            string _promotions = projectDirectory + "\\" + ConfigurationManager.AppSettings["promotionsRule"];
            promotionManager = new PromotionManager(_promotions);
            InitDataSource(DataSourceType.FILE, _url);
        }
        static void InitDataSource(DataSourceType dataSourceType, string source)
        {
            dataSource = new DataSource(dataSourceType, source);
            ProductMaster[] productMasters = dataSource.GetDataFromSource();
            ListMenu(productMasters);
        }
        static void ListMenu(ProductMaster[] productMasters)
        {
            bool bExit = true;
            int choice = -1;
            Order order = new Order();
            while (bExit)
            {
                Console.Clear();
                Console.WriteLine("*************** Product List *******************");
                Console.WriteLine("SKUID\tName\tPrice");
                foreach (ProductMaster product in productMasters)
                {
                    Console.WriteLine(product.SKUID + "\t" + product.ProductName + "\t" + product.Price);
                }
                Console.WriteLine("**************************************************\n\n");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Delete Product");
                Console.WriteLine("3. Print Bill With Promotions Applied");
                Console.WriteLine("4. Print Bill Without Promotions");
                Console.WriteLine("5. Exit");
                Console.WriteLine("Enter your choice...");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter SKU ID:");
                        string pID = Console.ReadLine();
                        Console.Write("Enter Quantity:");
                        long qty = Convert.ToInt64(Console.ReadLine());
                        ProductMaster product = dataSource.GetProductById(pID);
                        product.Quantity = qty;
                        order.AddProductToOrder(product);
                        break;
                    case 2:
                        break;
                    case 3:
                        List<ProductMaster> allOrers = order.GetAllOrder();
                        Console.WriteLine("SKUID \t Product Name \t Quantity \t Price");
                        Console.WriteLine("_____________________________________________");
                        foreach (ProductMaster productManager in allOrers)
                        {
                            Console.WriteLine(productManager.SKUID + "\t" + productManager.ProductName + "\t\t" + productManager.Quantity
                                + "\t\t" + productManager.Price);
                        }
                        Console.WriteLine("*********** END OF LIST ************************");
                        Console.ReadLine();
                        break;
                    case 4:
                        List<ProductMaster> allPromOrers = order.GetAllOrder();
                        allPromOrers = promotionManager.ApplyPromotion(allPromOrers, order);//Apply all the active Promotions
                        Console.WriteLine("SKUID \t Product Name \t Quantity \t Price");
                        Console.WriteLine("_____________________________________________");
                        foreach (ProductMaster productManager in allPromOrers)
                        {
                            Console.WriteLine(productManager.SKUID + "\t" + productManager.ProductName + "\t\t" + productManager.Quantity
                                + "\t\t" + productManager.Price);
                        }
                        Console.WriteLine("*********** END OF LIST ************************");
                        Console.ReadLine();
                        break;
                    case 5:
                        bExit = false;
                        break;
                }
            }
        }
    }
}

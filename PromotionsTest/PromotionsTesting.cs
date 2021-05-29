using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionsEngine;
using PromotionsEngine.Models;
using PromotionsEngine.Rules;
using System;
using System.Configuration;
using System.IO;

namespace PromotionsTest
{
    [TestClass]
    public class PromotionsTesting
    {
        static DataSource dataSource;
        static PromotionManager promotionManager;
        public PromotionsTesting()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string _url = projectDirectory + "\\ProductList.json";
            string _promotions = projectDirectory + "\\PromotionsRule.json";
            promotionManager = new PromotionManager(_promotions);
            InitDataSource(DataSourceType.FILE, _url);
        }
        void InitDataSource(DataSourceType dataSourceType, string source)
        {
            dataSource = new DataSource(dataSourceType, source);
            ProductMaster[] productMasters = dataSource.GetDataFromSource();
        }
        [TestMethod]
        public void AddProduct()
        {
            //Arrange
            ProductMaster productMaster = new ProductMaster();
            productMaster.ProductID = 1;
            productMaster.ProductName = "Apple";
            productMaster.Quantity = 10;
            productMaster.SKUID = "A";
            
            //Act
            Order order = new Order();
            order.AddProductToOrder(productMaster);

            //Assert
            ProductMaster product = order.GetOrderById("A");

            Assert.AreEqual(product.SKUID.ToLower(), productMaster.SKUID.ToLower());
        }
        [TestMethod]
        public void CheckProductExists()
        {
            //Arrange
            Order order = new Order();
            //Act
            ProductMaster product = order.GetOrderById("PP");
            //Assert
            Assert.AreEqual(product, null);
        }
    }
}

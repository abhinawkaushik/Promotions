using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionsEngine
{
    public class Order
    {
        public List<ProductMaster> orders;
        public Order()
        {
            orders = new List<ProductMaster>();
        }
        public void AddProductToOrder(ProductMaster product)
        {
            orders.Add(product);
        }
        public List<ProductMaster> GetAllOrder()
        {
            return orders;
        }
        public ProductMaster GetOrderById(string SKUID)
        {
            ProductMaster productMaster = null;
            foreach (ProductMaster product in orders)
            {
                if (product.SKUID.ToLower() == SKUID.ToLower())
                {
                    productMaster = product;
                    break;
                }
            }
            return productMaster;
        }
        public List<ProductMaster> ApplyPromotions()
        {
            List<ProductMaster> products = GetAllOrder();

            return products;
        }
    }
}

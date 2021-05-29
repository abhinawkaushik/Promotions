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
            ProductMaster productMaster = GetOrderById(product.SKUID);
            if (productMaster != null)
            {
                productMaster.Quantity += product.Quantity;
                RemoveOrderById(product.SKUID);
            }
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
        public bool SKUCombinationExists(string SKUIDs)
        {
            int count = 0;
            string[] products = SKUIDs.Split(',');
            for (int iCount = 0; iCount < products.Length; iCount++)
            {                
                foreach (ProductMaster product in orders)
                {
                    if (product.SKUID.ToLower() == products[iCount].ToLower())
                    {
                        count++;
                    }
                }
            }
            return products.Length == count;
        }
        public bool RemoveOrderById(string SKUID)
        {
            ProductMaster productMaster = null;
            bool bSuccess = false;
            foreach (ProductMaster product in orders)
            {
                if (product.SKUID.ToLower() == SKUID.ToLower())
                {
                    productMaster = product;
                    bSuccess = true;
                    break;
                }
            }
            orders.Remove(productMaster);
            return bSuccess;
        }
        public List<ProductMaster> ApplyPromotions()
        {
            List<ProductMaster> products = GetAllOrder();

            return products;
        }
    }
}

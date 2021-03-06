using Newtonsoft.Json;
using PromotionsEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionsEngine.Rules
{
    public class PromotionManager
    {
        private string _url;
        private Promotions[] Promotions;
        public PromotionManager(string url)
        {
            this._url = url;
            LoadPromotionsFromFILE();
        }
        private long LoadPromotionsFromFILE()
        {
            long lRetVal = ReturnCode.FAIL;
            string _promotionsData = System.IO.File.ReadAllText(this._url);
            try
            {
                Promotions = JsonConvert.DeserializeObject<Promotions[]>(_promotionsData);
                lRetVal = ReturnCode.SUCCESS;
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to load Data from the given Source", ex.InnerException);
            }
            return lRetVal;
        }
        public Promotions[] GetAllPromotions()
        {
            return Promotions;
        }
        public Promotions GetPromotionByType(PromotionType promotionType)
        {
            Promotions promotions = null;
            foreach (Promotions pro in Promotions)
            {
                if (pro.PromotionType == promotionType)
                {
                    promotions = pro;
                    break;
                }
            }
            return promotions;
        }
        public List<ProductMaster> ApplyPromotion(List<ProductMaster> productMasters, Order order)
        {
            List<ProductMaster> newOrder = new List<ProductMaster>();
            ProductMaster productMaster = new ProductMaster();
            foreach (Promotions promotions in Promotions)
            {
                switch (promotions.PromotionType)
                {
                    case PromotionType.ByProduct:
                        string[] SKUID = promotions.SKUID.Split(',');
                        productMaster = ApplyDiscount(SKUID[0], promotions, order);
                        newOrder.Add(productMaster);
                        break;
                    case PromotionType.ByMultipleProducts:
                        SKUID = promotions.SKUID.Split(',');
                        //First Check if Products are available in order to be eligible for Promotions
                        if (order.SKUCombinationExists(promotions.SKUID))
                        {
                            for (int iCount = 0; iCount < SKUID.Length; iCount++)
                            {
                                productMaster = ApplyDiscount(SKUID[iCount], promotions, order);
                                if (SKUID.Length != iCount)
                                {
                                    productMaster.Price = 0;
                                }
                                newOrder.Add(productMaster);
                            }
                        }
                        else
                        {
                            for (int iCount = 0; iCount < SKUID.Length; iCount++)
                            {
                                productMaster = order.GetOrderById(SKUID[iCount]);
                                if (productMaster != null)
                                {
                                    newOrder.Add(productMaster);
                                }                                
                            }
                        }
                        break;
                    case PromotionType.ByPercent:
                        break;
                    case PromotionType.ByQuantity:
                        break;
                    default:
                        break;
                }
            }
            return newOrder;
        }
        private ProductMaster ApplyDiscount(string SKUID, Promotions promotions, Order order)
        {
            ProductMaster productMaster = null;
            productMaster = order.GetOrderById(SKUID);
            switch (promotions.PromotionType)
            {
                case PromotionType.ByProduct:
                    long Qty = Convert.ToInt64(promotions.Quantity);
                    decimal price = promotions.Price;
                    long multiple = (int)productMaster.Quantity / Qty;
                    long reminder = (int)productMaster.Quantity % Qty;
                    decimal _finalPrice = (price * multiple) + reminder * productMaster.Price;
                    productMaster.Price = _finalPrice;
                    break;
                case PromotionType.ByMultipleProducts:                 
                    productMaster.Price = promotions.Price;
                    break;
                case PromotionType.ByPercent:
                    break;
                case PromotionType.ByQuantity:
                    break;
                default:
                    break;
            }
            
            return productMaster;
        }        
    }
}

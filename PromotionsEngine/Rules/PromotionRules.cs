using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionsEngine.Rules
{
    public enum PromotionType
    {
        ByProduct = 1,
        ByMultipleProducts = 2,
        ByPercent = 3,
        ByQuantity = 4
    }
    public class Promotions
    {
        public PromotionType PromotionType;
        public string SKUID;
        public string Quantity;
        public decimal Price;
    }
}

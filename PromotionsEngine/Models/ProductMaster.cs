using System;

namespace PromotionsEngine
{
    public class ProductMaster
    {
        long _productID;
        string _productName;
        long _quantity;
        decimal _price;
        public long ProductID
        {
            get { return _productID; }
            set { _productID = value; }
        }
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }
        public long Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }
    }
}

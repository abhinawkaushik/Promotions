using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionsEngine.Interface
{
    public interface IDataFactory
    {
        IData CreateData();
    }
    public interface IData
    {
        ProductMaster[] GetProducts();
        ProductMaster GetProductById(string SKUID);
    }
}

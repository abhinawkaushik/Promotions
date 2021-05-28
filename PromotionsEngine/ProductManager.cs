using System;
using System.Collections.Generic;
using System.Text;
using PromotionsEngine.Interface;
using PromotionsEngine.Models;
using Newtonsoft.Json;

namespace PromotionsEngine
{
    public class ProductManager : IData
    {
        private string _url;
        private DataSourceType _dataSourceType;
        private ProductMaster[] allProducts;
        public ProductManager(string url, DataSourceType dataSourceType)
        {
            this._url = url;
            this._dataSourceType = dataSourceType;
            InitProductMaster();
        }
        public ProductMaster[] GetProducts()
        {
            return allProducts;
        }
        public ProductMaster GetProductById(string SKUID)
        {
            ProductMaster productMaster = null;
            foreach (ProductMaster product in allProducts)
            {
                if (product.SKUID.ToLower() == SKUID.ToLower())
                {
                    productMaster = product;
                    break;
                }
            }
            return productMaster;
        }
        private void InitProductMaster()
        {
            switch (this._dataSourceType)
            {
                case DataSourceType.FILE:
                    LoadProductsFromFILE();
                    break;
                case DataSourceType.ORACLEB:
                    LoadProductsFromOracleDB();
                    break;
                case DataSourceType.SQLDB:
                    LoadProductsFromSQLDB();
                    break;
            }
        }
        private long LoadProductsFromSQLDB()
        {
            long lRetVal = ReturnCode.FAIL;

            return lRetVal;
        }
        private long LoadProductsFromOracleDB()
        {
            long lRetVal = ReturnCode.FAIL;

            return lRetVal;
        }
        private long LoadProductsFromFILE()
        {
            long lRetVal = ReturnCode.FAIL;
            string _productData = System.IO.File.ReadAllText(this._url);
            try
            {
                allProducts = JsonConvert.DeserializeObject<ProductMaster[]>(_productData);
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to load Data from the given Source", ex.InnerException);
            }
            return lRetVal;
        }
    }
}

using PromotionsEngine.Factory;
using PromotionsEngine.Interface;
using PromotionsEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionsEngine
{
    public class DataSource
    {
        private IData _data;
        public DataSource(DataSourceType dataType, string source)
        {
            IDataFactory factory;
            switch (dataType)
            {
                case DataSourceType.FILE:
                    factory = new DataFromFile(source);
                    _data = factory.CreateData();
                    break;
                case DataSourceType.SQLDB:
                    factory = new DataFromSQLDB(source);
                    _data = factory.CreateData();
                    break;
                case DataSourceType.ORACLEB:
                    factory = new DataFromOracleDB(source);
                    _data = factory.CreateData();
                    break;
            }
        }

        public ProductMaster[] GetDataFromSource()
        {
            return _data.GetProducts();
        }
        public ProductMaster GetProductById(string SKUID)
        {
            return _data.GetProductById(SKUID);
        }
    }
}

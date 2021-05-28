using PromotionsEngine.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionsEngine.Factory
{
    public class DataFromOracleDB : IDataFactory
    {
        private string _sourceFileURL;
        public DataFromOracleDB(string sourceURL)
        {
            this._sourceFileURL = sourceURL;
        }
        public IData CreateData()
        {
            return new ProductManager(this._sourceFileURL, Models.DataSourceType.ORACLEB);
        }
    }
}

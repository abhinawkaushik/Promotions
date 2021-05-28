using PromotionsEngine.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionsEngine.Factory
{
    public class DataFromSQLDB: IDataFactory
    {
        private string _sourceFileURL;
        public DataFromSQLDB(string url)
        {
            this._sourceFileURL = url;
        }
        public IData CreateData()
        {
            return new ProductManager(this._sourceFileURL, Models.DataSourceType.SQLDB);
        }
    }
}

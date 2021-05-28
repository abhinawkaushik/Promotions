using PromotionsEngine.Interface;
using PromotionsEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionsEngine.Factory
{
    public class DataFromFile: IDataFactory
    {
        private string _sourceFileURL;
        public DataFromFile(string sourceURL)
        {
            this._sourceFileURL = sourceURL;
        }
        public IData CreateData()
        {
            return new ProductManager(this._sourceFileURL, DataSourceType.FILE);
        }
    }
}

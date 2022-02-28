using Dicon.Project.Switch.Test.Infrastructure.Connection;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dicon.Project.Switch.Test.Infrastructure.Repository
{
    public interface IProductInfoRepository
    {
        IEnumerable<ProductInfo> ReadRange();
    }

    public class ProductInfoRepository : DapperGenericRepository<ProductInfo>, IProductInfoRepository
    {

        public ProductInfoRepository(IDBConnERMeasurementFactory connFactory) : base(connFactory)
        {
        }

        public ProductInfoRepository(Func<IDbTransaction> transaction, int? commandTimeout = null) : base(transaction, commandTimeout)
        {
        }

    }
}

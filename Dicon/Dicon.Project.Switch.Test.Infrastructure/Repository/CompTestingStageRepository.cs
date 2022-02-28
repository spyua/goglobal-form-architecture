using Dapper;
using System.Collections.Generic;
using System.Data;
using Dapper.Contrib.Extensions;
using System.Text;
using Dicon.Project.Switch.Test.Infrastructure.Connection;

namespace Dicon.Project.Switch.Test.Infrastructure.Repository
{

    public interface ICompTestingStageRepository
    {
        void Add(COMP_TESTING_STAGE entity);

        IEnumerable<COMP_TESTING_STAGE> Read(string snNo);

        void Update(COMP_TESTING_STAGE entity);

    }

    public class CompTestingStageRepository : DapperGenericRepository<COMP_TESTING_STAGE>, ICompTestingStageRepository
    {
 
        public CompTestingStageRepository(IDBConn1XNFactory transaction) : base(transaction)
        {
        }

        
        public override void Add(COMP_TESTING_STAGE member) => Connection.Insert(member, Transaction, CommandTimeout);

        
        public IEnumerable<COMP_TESTING_STAGE> Read(string snNo)
        {
            var sqlStr = new StringBuilder();
            sqlStr.Append(" SELECT * FROM ");
            sqlStr.Append(nameof(COMP_TESTING_STAGE));
            sqlStr.Append($" Where {nameof(COMP_TESTING_STAGE.COMP_SN)} = '{snNo}'");

            return Connection.Query<COMP_TESTING_STAGE>(sqlStr.ToString());
        }

        public COMP_TESTING_STAGE ReadOne(string snNo)
        {
            var sqlStr = new StringBuilder();
            sqlStr.Append(" SELECT * FROM ");
            sqlStr.Append(nameof(COMP_TESTING_STAGE));
            sqlStr.Append($" Where {nameof(COMP_TESTING_STAGE.COMP_SN)} = '{snNo}'");

            return Connection.QueryFirstOrDefault<COMP_TESTING_STAGE>(sqlStr.ToString());
        }
    }
}

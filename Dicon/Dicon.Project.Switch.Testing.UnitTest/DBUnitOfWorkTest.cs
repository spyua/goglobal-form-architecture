using Dicon.Project.Switch.Test.Infrastructure.Connection;
using NUnit.Framework;
using System.Data;

namespace Dicon.Project.Switch.Testing.UnitTest
{

    public class DBUnitOfWorkTest
    {
        private IDbConnection dbConnection;

        [SetUp]
        public void Setup()
        {
            var connStr = "Data Source=GFI_E0373\\SQLEXPRESS;Initial Catalog=1XN_MEMS_SWITCH_DB;Persist Security Info=True;User ID=sa;Password=laser99";
            var sqlFactory = new SqlDbFactory(connStr);
            dbConnection = sqlFactory.Create();
        }



    }
}

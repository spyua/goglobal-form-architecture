using Dicon.Project.Switch.Test.Infrastructure.BaseUnitOfWork;
using Dicon.Project.Switch.Test.Infrastructure.Connection;
using Dicon.Project.Switch.Test.Infrastructure.Repository;
using NUnit.Framework;
using System.Data;

namespace Dicon.Project.Switch.Testing.UnitTest
{

    public class DBUnitOfWorkTest
    {
        private DapperUnitOfWork Uow;
        private IDbConnection dbConnection;

        private string LocalConnStr(string dbName)
        {
            return $"Data Source=LAPTOP-J0A685IR\\MSSQLSERVER01;Initial Catalog={dbName};Persist Security Info=True;User ID=sa;Password=laser99";
        }

        [SetUp]
        public void Setup()
        {
            //var connStr = "Data Source=GFI_E0373\\SQLEXPRESS;Initial Catalog=1XN_MEMS_SWITCH_DB;Persist Security Info=True;User ID=sa;Password=laser99";
            var sqlFactory = new SqlDbFactory(LocalConnStr("Dummy"));
            dbConnection = sqlFactory.Create();
            Uow = new DapperUnitOfWork(dbConnection);
        }

        [Test]
        public void When_CompTestingStageRepo_create_dat_it_can_read_value()
        {
            var userRepo = Uow.GetGenericRepository<users>();

            var user = new users()
            {
                id = 12,
                name = "123",
                gender = "123",
                age = 11,
                hobby = "123"
            };

            userRepo.Add(user);


            Uow.Save();
        }

    }
}

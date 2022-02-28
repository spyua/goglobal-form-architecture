using Dicon.Project.Switch.Test.Infrastructure.BaseRepository;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.SqlClient;

namespace Dicon.Project.Switch.Test.Infrastructure.Connection
{
    // 自行實作

    //1XN DB
    public interface IDBConn1XNFactory : IDbConnectionFactory
    {

    }

    //ERMeasurement DB
    public interface IDBConnERMeasurementFactory: IDbConnectionFactory
    {

    }
 
    //SQLLite
    public class SQLiteDbFactory : IDBConn1XNFactory, IDBConnERMeasurementFactory
    {
        private string ConnectString { get; set; }

        public SQLiteDbFactory(string connectString)
        {
            ConnectString = connectString;
        }

        public IDbConnection Create()
        {
            return new SqliteConnection(ConnectString);
        }
    }

    // MSSQL
    public class SqlDbFactory : IDBConn1XNFactory, IDBConnERMeasurementFactory
    {
        private string ConnectString { get; set; }

        public SqlDbFactory(string connectString)
        {
            ConnectString = connectString;
        }

        public IDbConnection Create()
        {
            return new SqlConnection(ConnectString);
        }
    }
}

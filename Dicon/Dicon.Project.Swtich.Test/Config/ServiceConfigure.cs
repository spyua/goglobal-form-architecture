using Dicon.Project.Switch.Test.Infrastructure.Connection;
using Dicon.Project.Switch.Test.Infrastructure.Repository;
using Dicon.Project.Swtich.Test;
using Dicon.Project.Swtich.Testing.Controller;
using Dicon.Project.Swtich.Testing.Core.Help;
using Microsoft.Extensions.DependencyInjection;

namespace Dicon.Project.Swtich.Testing.Config
{
    /// <summary>
    /// DI Serice Configure (註冊所有專案使用物件)
    /// </summary>
    public static class ServiceConfigure
    {
        private static ServiceProvider _provider;

        public static ServiceProvider GetProvider()
        {
            if (_provider is null) _provider = CreateServiceProvider();
            return _provider;
        }

        private static ServiceProvider CreateServiceProvider()
        {
            var collection = new ServiceCollection();

            // Register auto map
            collection.AddSingleton<AutoMapperProfile>();
            collection.AddSingleton<AutoMapperFactory>();
            // Automap Create
            collection.AddSingleton(c =>
            {
                var IMap = c.GetService<AutoMapperFactory>().Create();
                return IMap;
            });


            // Register DB Connection          
            collection.AddScoped<IDBConn1XNFactory, SqlDbFactory>(c =>
            {
                return new SqlDbFactory(AppSetting.Instance.MSSQL1XNDBCon);
            });
            collection.AddTransient(c => c.GetService<IDBConn1XNFactory>().Create());

            collection.AddScoped<IDBConnERMeasurementFactory, SqlDbFactory>(c =>
            {
                return new SqlDbFactory(AppSetting.Instance.MSSQLERMDBCon);
            });
            collection.AddTransient(c => c.GetService<IDBConnERMeasurementFactory>().Create());

            // Register Repository
            collection.AddTransient<ICompTestingStageRepository, CompTestingStageRepository>();
            collection.AddTransient<IProductInfoRepository, ProductInfoRepository>();

            // Register Controller
            collection.AddTransient<IRepositoryController, RepositoryController>();
            // 宣告Form1
            collection.AddScoped<MainForm>();


            return collection.BuildServiceProvider();
        }
    }
}

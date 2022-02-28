using Dicon.Project.Switch.Test.Infrastructure.Repository;
using System;
using System.Collections;
using System.Data;
using System.Linq;

namespace Dicon.Project.Switch.Test.Infrastructure.BaseUnitOfWork
{
    /// <summary>    
    /// 使用反射取得Repository來使用
    /// </summary>
    public class DapperUnitOfWork : UnitOfWorkTemplate, IDapperUnitOfWork
    {
        private Hashtable Repositories { get; set; } = new Hashtable();

        public DapperUnitOfWork(IDbConnection connection) : base(connection)
        {
        }

        public TRepository GetRepository<TRepository>() where TRepository : class
        {
            var type = typeof(TRepository);
            var typeName = type.Name;

            if (!Repositories.ContainsKey(typeName))
            {
                var types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);

                if (types.Count() == 0) throw new ArgumentNullException($"Can't find the {typeName}");
                if (types.Count() > 1) throw new ArgumentOutOfRangeException($"{typeName} more than one");

                var repositoryInstance = Activator.CreateInstance(types.Single(), new Func<IDbTransaction>(() => Transaction), CommandTimeout);
                Repositories.Add(typeName, repositoryInstance);
            }

            return (TRepository)Repositories[typeName];
        }
        public IDapperGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : class, new()
        {
            var type = typeof(TEntity).Name;

            if (!Repositories.ContainsKey(type))
            {
                var repositoryType = typeof(DapperGenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), new Func<IDbTransaction>(() => Transaction), CommandTimeout);
                Repositories.Add(type, repositoryInstance);
            }

            return (IDapperGenericRepository<TEntity>)Repositories[type];
        }

        protected override void Disposing()
        {
            Repositories.Clear();
            Repositories = null;
        }
    }
}

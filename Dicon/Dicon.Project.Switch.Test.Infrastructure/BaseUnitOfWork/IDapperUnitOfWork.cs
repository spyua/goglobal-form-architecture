using Dicon.Project.Switch.Test.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dicon.Project.Switch.Test.Infrastructure.BaseUnitOfWork
{
    /// <summary>
    /// Dapper Unit Of Work
    /// </summary>
    public interface IDapperUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// 取得泛型(Table版本)
        /// </summary>
        IDapperGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : class, new();
    }
}

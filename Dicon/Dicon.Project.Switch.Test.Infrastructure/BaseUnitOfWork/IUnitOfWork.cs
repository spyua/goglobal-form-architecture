using System;

namespace Dicon.Project.Switch.Test.Infrastructure.BaseUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 儲存
        /// </summary>
        void Save();
    }
}

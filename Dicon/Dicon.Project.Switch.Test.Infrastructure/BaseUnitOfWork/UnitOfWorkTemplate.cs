using System;
using System.Data;


namespace Dicon.Project.Switch.Test.Infrastructure.BaseUnitOfWork
{
    /// <summary>
    /// UnitOfWork共用抽象類別
    /// </summary>
    public abstract class UnitOfWorkTemplate : IUnitOfWork
    {
        /// <summary>
        /// DB連線
        /// </summary>
        protected IDbConnection Connection { get; private set; }

        /// <summary>
        /// DB交易
        /// </summary>
        protected IDbTransaction Transaction { get; private set; }

        /// <summary>
        /// 命令超時時間
        /// </summary>
        protected int? CommandTimeout { get; private set; }

        private bool _disposed = false;

        public UnitOfWorkTemplate(IDbConnection connection)
        {
            Connection = connection;
            Connection.Open();
            Transaction = Connection.BeginTransaction();
        }

        ~UnitOfWorkTemplate()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (Transaction != null)
                    {
                        Transaction.Dispose();
                        Transaction = null;
                    }
                    if (Connection != null)
                    {
                        Connection.Close();
                        Connection.Dispose();
                        Connection = null;
                    }

                    Disposing();
                }
                _disposed = true;
            }
        }

        public void Save()
        {
            try
            {
                Transaction.Commit();
            }
            catch
            {
                Transaction.Rollback();
                throw;
            }
            finally
            {
                // 由於Commit和Rollback後，會將交易取消，因此釋放交易後，再重新創建交易
                Transaction.Dispose();
                Transaction = Connection.BeginTransaction();
            }
        }
        protected virtual void Disposing() { }
    }
}

namespace Dicon.Project.Switch.Test.Infrastructure.Repository
{
    public interface IDapperGenericRepository<TEntity> : IGenericRepository<TEntity>
     where TEntity : class, new()
    {
        // 預留定義其他常用的功能介面
        // 或使用Extension類別擴充        
    }
}

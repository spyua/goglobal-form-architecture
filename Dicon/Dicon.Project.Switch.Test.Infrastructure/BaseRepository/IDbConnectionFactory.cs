using System.Data;

namespace Dicon.Project.Switch.Test.Infrastructure.BaseRepository
{
    public interface IDbConnectionFactory
    {
        IDbConnection Create();
    }

}

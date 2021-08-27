using Microsoft.EntityFrameworkCore;

namespace Common.System.Infrastructure.Repositories
{
    public class Repository<TEntity> : BaseRepository<TEntity> where TEntity : class
	{
		public Repository(DbContext dbContext)
			: base(dbContext)
		{
		}
	}
}

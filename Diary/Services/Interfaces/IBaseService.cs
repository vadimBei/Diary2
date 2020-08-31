using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Services.Interfaces
{
	public interface IBaseService<TEntity> where TEntity : class
	{
		Task<IEnumerable<TEntity>> GetAllAsync();

		Task<TEntity> GetByIdAsync(Guid id);

		Task<TEntity> CreateAsync(TEntity createObject);

		Task UpdateAsync(TEntity updateObject);

		Task<bool> DeleteAsync(Guid id);
	}
}

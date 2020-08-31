using System;
using System.Collections.Generic;
using System.Text;

namespace Diary.Repository
{
	public class BlogRepository<TEntity> : BaseRepository<TEntity> where TEntity : class
	{
		public BlogRepository(DiaryContext context)
			: base(context)
		{

		}
	}
}

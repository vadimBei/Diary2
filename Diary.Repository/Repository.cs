using System;
using System.Collections.Generic;
using System.Text;

namespace Diary.Repository
{
	public class Repository<TEntity> : BaseRepository<TEntity> where TEntity : class
	{
		public Repository(DiaryContext diaryContext)
			: base(diaryContext)
		{

		}
	}
}

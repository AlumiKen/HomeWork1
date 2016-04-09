using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace homework1.Models
{
	public static class RepositoryIQueryableExtensions
	{
		public static IQueryable<T> Include<T>
			(this IQueryable<T> source, string path) where T : class
		{
			//var objectQuery = source as ObjectQuery<T>;
			if (source is ObjectQuery<T> || source is DbSet<T> || source is DbQuery || source is DbSet )
			{
				return source.Include(path);
			}
			return source;
		}
	}
}
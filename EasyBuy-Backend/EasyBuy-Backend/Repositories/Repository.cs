using EasyBuy_Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace EasyBuy_Backend.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MyDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(MyDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

		public bool Create(T entity)
		{
			_dbSet.Add(entity);
			return _context.SaveChanges() > 0; 
		}

		public bool Update(T entity)                    
		{
			_dbSet.Update(entity);
			return _context.SaveChanges() > 0; 
		}

		public bool Delete(T entity)
		{
			_dbSet.Remove(entity);
			return _context.SaveChanges() > 0; 
		}
	}
}

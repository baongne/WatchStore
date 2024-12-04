namespace EasyBuy_Backend.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        bool Create(T entity);

		bool Update(T entity);

		bool Delete(T entity);
    }
}

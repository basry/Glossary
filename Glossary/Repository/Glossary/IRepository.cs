using System.Threading.Tasks;

namespace Glossary.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);

        Task<TEntity> Get(string id);

        void Remove(string id);

    }
}

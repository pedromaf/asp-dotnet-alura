using FilmesAPI.Models.Entities;

namespace FilmesAPI.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public void Add(TEntity entity);
        public void Delete(TEntity entity);
        public void Update(TEntity entity);
        public TEntity GetById(int id);
        public List<TEntity> GetAll();
    }
}

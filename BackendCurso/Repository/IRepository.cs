namespace BackendCurso.Repository
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> GetById(int id);
        Task Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task Save();

        // Un enumerable que regrese la entidad que se esta buscando TEntity
        // Un Func que recibe una entidad y regresa un booleano
        IEnumerable<TEntity> Search(Func<TEntity, bool> filter);

    }
}

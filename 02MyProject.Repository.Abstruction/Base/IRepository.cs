namespace MyProject.Repository.Abstructions.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ICollection<TEntity> GetAll();
        Task<bool> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        public bool Remove(TEntity entity);
    }
}

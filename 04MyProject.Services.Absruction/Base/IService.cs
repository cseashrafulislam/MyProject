namespace MyProject.Services.Absructions.Base
{
    public interface IService<TEntity> where TEntity : class
    {
        ICollection<TEntity> GetAll();
        Task<bool> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        bool Remove(TEntity entity);
    }
}

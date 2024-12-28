using MyProject.Repository.Abstructions.Base;
using MyProject.Services.Absructions.Base;

namespace MyProject.Services.Base
{
    public class BaseService<TEntity> : IService<TEntity> where TEntity : class
    {
        IRepository<TEntity> _repository;

        public BaseService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<bool> Add(TEntity entity)
        {
            return await _repository.Add(entity);
        }

        public virtual ICollection<TEntity> GetAll()
        {
            return _repository.GetAll();
        }



        public virtual bool Remove(TEntity entity)
        {
            return _repository.Remove(entity);
        }

        public async virtual Task<bool> Update(TEntity entity)
        {
            return await _repository.Update(entity);
        }
    }
}

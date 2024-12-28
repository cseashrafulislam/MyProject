using Microsoft.EntityFrameworkCore;
using MyProject.Repository.Abstructions.Base;

namespace MyProject.Repositories.BaseRepository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _db;

        public BaseRepository(DbContext db)
        {
            _db = db;
        }

        private DbSet<TEntity> Table => _db.Set<TEntity>();

        public virtual ICollection<TEntity> GetAll()
        {
            return Table.Where(e => EF.Property<bool>(e, "Deleted") == false).ToList();
        }

        public virtual async Task<bool> Add(TEntity entity)
        {
            Table.Add(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async virtual Task<bool> Update(TEntity entity)
        {
            Table.Update(entity);
            return await _db.SaveChangesAsync()>0;
        }

        public virtual bool Remove(TEntity entity)
        {
            var deletedProperty = typeof(TEntity).GetProperty("Deleted");
            if (deletedProperty != null && deletedProperty.PropertyType == typeof(bool))
            {
                deletedProperty.SetValue(entity, true); // Set Deleted = true
                Table.Update(entity);
            }
            else
            {
                Table.Remove(entity);
            }
            return _db.SaveChanges() > 0;
        }
    }

}

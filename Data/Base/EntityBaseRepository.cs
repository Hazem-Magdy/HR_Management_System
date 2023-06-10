using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Reflection;

namespace HR_Management_System.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext db;
        public EntityBaseRepository(AppDbContext _db)
        {
            db = _db;
        }

        //public async Task<List<T>> GetAllAsync() => await db.Set<T>().ToListAsync();


        //public async Task<T> GetByIDAsync(int id) => await db.Set<T>().FirstOrDefaultAsync(a => a.Id == id);
        //public async Task<T> GetByIDAsync(int id, params Expression<Func<T, object>>[] includeProperties)
        //{
        //    IQueryable<T> query = db.Set<T>();

        //    foreach (var includeProperty in includeProperties)
        //    {
        //        query = query.Include(includeProperty);
        //    }

        //    return await query.FirstOrDefaultAsync(a => a.Id == id);
        //}

        public async Task<T> AddAsync(T entity)
        {
            await db.Set<T>().AddAsync(entity);
            db.SaveChanges();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await db.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityEntry = db.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;

            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, T entity)
        {
            // Get the existing entity with the same 'Id' value, if any
            var existingEntity = await db.Set<T>().FindAsync(id);

            if (existingEntity != null)
            {
                // Detach the existing entity from the context to avoid conflicts
                db.Entry(existingEntity).State = EntityState.Detached;
            }

            // Attach the new entity to the context
            db.Entry(entity).State = EntityState.Modified;

            await db.SaveChangesAsync();
        }

        //public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        //{
        //    IQueryable<T> query = db.Set<T>();
        //    query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        //    return await query.ToListAsync();
        //}

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = db.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                if (includeProperty.Body is MemberExpression memberExpression)
                {
                    if (memberExpression.Member is PropertyInfo propertyInfo)
                    {
                        query = query.Include(propertyInfo.Name);
                    }
                }
                else if (includeProperty.Body is UnaryExpression unaryExpression)
                {
                    if (unaryExpression.Operand is MemberExpression operandExpression && operandExpression.Member is PropertyInfo operandPropertyInfo)
                    {
                        query = query.Include(operandPropertyInfo.Name);
                    }
                }
            }

            return await query.ToListAsync();
        }


        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = db.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(a => a.Id == id);
            //IQueryable<T> query = db.Set<T>();
            //query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            //return await query.FirstOrDefaultAsync(n => n.Id == id);
        }




    }
}

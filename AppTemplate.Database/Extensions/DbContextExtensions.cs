using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;

namespace AppTemplate.Database.Extensions
{
    public class Changes<TEntity, TModel>
    {
        public Changes()
        {
            Added = Enumerable.Empty<TModel>();
            Updated = ImmutableDictionary.Create<TEntity, TModel>();
            Deleted = Enumerable.Empty<TEntity>();
        }

        public IEnumerable<TModel> Added { get; set; }

        public IReadOnlyDictionary<TEntity, TModel> Updated { get; set; }

        public IEnumerable<TEntity> Deleted { get; set; }
    }

    public static class DbContextExtensions
    {
        public static Changes<TEntity, TModel> GetChanges<TEntity, TModel>(
             this ICollection<TEntity> entities,
             ICollection<TModel> models,
             Func<TEntity, TModel, bool> compare)
        {
            var result = new Changes<TEntity, TModel>();

            if (entities == null)
                return result;

            if (models?.Any() != true)
            {
                result.Deleted = entities.ToList();
                return result;
            }

            result.Deleted = entities
                .Where(entity => models.All(model => !compare(entity, model)))
                .ToList();

            var updated = new Dictionary<TEntity, TModel>();

            foreach (var model in models)
            {
                var entity = entities.SingleOrDefault(e => compare(e, model));
                if (entity != null)
                    updated.Add(entity, model);
            }

            result.Updated = updated;

            result.Added = models
                .Where(model => entities.All(entity => !compare(entity, model)))
                .ToList();

            return result;
        }

        public static Changes<TEntity, TModel> RemoveDeleted<TEntity, TModel>(
            this Changes<TEntity, TModel> changes,
            DbSet<TEntity> dbSet)
            where TEntity : class
        {
            foreach (var entity in changes.Deleted)
            {
                dbSet.Remove(entity);
            }

            changes.Deleted = Enumerable.Empty<TEntity>();
            return changes;
        }

        public static Changes<TEntity, TModel> UpdateChanged<TEntity, TModel>(
            this Changes<TEntity, TModel> changes,
            Action<TEntity, TModel> mapToEntity)
            where TEntity : class
        {
            foreach (var model in changes.Updated)
            {
                mapToEntity(model.Key, model.Value);
            }

            changes.Updated = ImmutableDictionary.Create<TEntity, TModel>();
            return changes;
        }

        public static Changes<TEntity, TModel> CreateAdded<TEntity, TModel>(
            this Changes<TEntity, TModel> changes,
            DbSet<TEntity> dbSet,
            Action<TModel, TEntity> mapToEntity)
            where TEntity : class, new()
        {
            foreach (TModel model in changes.Added)
            {
                var entity = new TEntity();
                mapToEntity(model, entity);
                dbSet.Add(entity);
            }

            changes.Added = Enumerable.Empty<TModel>();
            return changes;
        }
    }
}

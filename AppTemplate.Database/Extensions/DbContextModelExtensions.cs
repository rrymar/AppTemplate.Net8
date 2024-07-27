using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppTemplate.Database.Extensions
{
    public static class DbContextModelExtensions
    {
        public static void ApplyConventions(this ModelBuilder modelBuilder)
        {
            modelBuilder.DisableCascades();
            modelBuilder.CreateIsActiveIndex();
            modelBuilder.SetAuditDefaultValues();
            modelBuilder.SetDatetimeUTC();
        }

        private static void SetDatetimeUTC(this ModelBuilder modelBuilder)
        {
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                        property.SetValueConverter(dateTimeConverter);
                }
            }
        }

        private static void DisableCascades(this ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                   .SelectMany(t => t.GetForeignKeys())
                   .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void CreateIsActiveIndex(this ModelBuilder modelBuilder)
        {
            var types = GetTypes(modelBuilder, typeof(AuditEntity));
            foreach (var type in types)
            {
                modelBuilder.Entity(type.ClrType)
                    .HasIndex(nameof(AuditEntity.IsActive));
            }
        }

        private static IEnumerable<IMutableEntityType> GetTypes(ModelBuilder modelBuilder, Type byInterface)
        {
            return modelBuilder.Model.GetEntityTypes()
                .Where(t => (byInterface.IsAssignableFrom(t.ClrType)));
        }

        private static void SetAuditDefaultValues(this ModelBuilder modelBuilder)
        {
            var types = GetTypes(modelBuilder, typeof(IAuditEntity));
            foreach (var type in types)
            {
                modelBuilder.Entity(type.ClrType)
                    .Property(nameof(AuditEntity.CreatedOn))
                    .HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                modelBuilder.Entity(type.ClrType)
                    .Property(nameof(AuditEntity.UpdatedOn))
                    .HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();
            }
        }
    }
}

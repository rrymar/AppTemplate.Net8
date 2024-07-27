using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppTemplate.Database
{
    public interface IAuditEntity
    {
        DateTime CreatedOn { get; set; }
        
        public int? CreatedById { get; set; }

        DateTime UpdatedOn { get; set; }
        
        public int? UpdatedById { get; set; }
    }

    public class AuditEntity: IAuditEntity
    {
        [Key]
        public int Id { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
        
        public int? CreatedById { get; set; }

        [ForeignKey(nameof(CreatedById))]
        public User CreatedBy { get; set; }

        public int? UpdatedById { get; set; }

        [ForeignKey(nameof(UpdatedById))]
        public User UpdatedBy { get; set; }
    }

    public static class AuditEntityExtensions
    {
        public static void Deactivate(this AuditEntity entity)
        {
            entity.IsActive = false;
        }
    }
}
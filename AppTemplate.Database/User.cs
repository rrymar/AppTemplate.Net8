using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AppTemplate.Net8.Database
{
    public class User : AuditEntity
    {
        [StringLength(32)]
        public string Username { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public string FullName { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(e => e.Username);

            modelBuilder.Entity<User>().Property(p => p.FullName)
                .HasComputedColumnSql($"CONCAT({nameof(FirstName)},' ', {nameof(LastName)})");
        }
    }

    public static class KnownUsers
    {
        public const int System = 1;

        public static readonly int[] SystemUsers = { System };
    }
}
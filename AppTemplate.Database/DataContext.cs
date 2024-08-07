﻿using AppTemplate.Database.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AppTemplate.Database;

public class DataContext(DbContextOptions options, ICurrentUserLocator? currentUserLocator = null)
    : DbContext(options)
{  
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConventions();
        User.OnModelCreating(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        OnBeforeSaving();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
    {
        if (currentUserLocator == null)
            throw new ApplicationException("CurrentUserLocator is not registered");
        
        foreach (var change in ChangeTracker.Entries().ToList())
        {
            if (change is not { Entity: AuditEntity auditEntity }) 
                continue;

            if (change.State is not (EntityState.Added or EntityState.Modified))
                continue;

            if (change.State == EntityState.Added)
            {
                auditEntity.CreatedById = currentUserLocator.UserId;
                auditEntity.CreatedOn = DateTime.UtcNow;
            }

            auditEntity.UpdatedById = currentUserLocator.UserId;
            auditEntity.UpdatedOn = DateTime.UtcNow;
        }
    }
}
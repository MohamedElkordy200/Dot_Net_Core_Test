using Lean.Domain.Repositories;
using System.Data;
using Lean.Contracts.Administration;
using Lean.Contracts.MessageModels;
using Microsoft.EntityFrameworkCore;
using Lean.Domain.DBEntities.Base;

namespace Lean.Persistence.Repositories
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<MessageModel> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                AuditEntities();
                var result = await _dbContext.SaveChangesAsync(cancellationToken);
                if (result> 0)
                {
                    return new SuccessMessage(RM.Common.SaveDone);
                }
                else
                {
                    return new SuccessMessage(RM.Common.NoChangeInRecords);
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }

        private void AuditEntities()
        {
            var entries = _dbContext.ChangeTracker.Entries()
                .Where(e => e.Entity is AuditEntity<Guid> && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditEntity<Guid>)entityEntry.Entity).Id = Guid.Empty;
                    ((AuditEntity<Guid>)entityEntry.Entity).CreatedAt = DateTime.Now;
                    ((AuditEntity<Guid>)entityEntry.Entity).CreatedBy =
                        UserIdentity.UserId; 
                }
                else
                {
                    entityEntry.Property("Id").IsModified = false;
                    entityEntry.Property("OrderIndex").IsModified = false;
                    entityEntry.Property("CreatedAt").IsModified = false;
                    entityEntry.Property("CreatedBy").IsModified = false;
                    ((AuditEntity<Guid>)entityEntry.Entity).ModifiedAt = DateTime.Now;
                    ((AuditEntity<Guid>)entityEntry.Entity).ModifiedBy =
                        UserIdentity.UserId;
                }
            }
        }
    }
}
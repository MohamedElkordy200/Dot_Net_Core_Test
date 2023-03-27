 
using Lean.Domain.DBEntities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Lean.Persistence.Configurations._Common
{
    internal static class CommonConfiguration
    {
        internal static void ConfigureAuditEntity<T>(EntityTypeBuilder<T> builder) where T : AuditEntity<Guid>
        {
            builder.Property(a => a.Id).HasDefaultValueSql("NEWID()");
            builder.Property(a => a.CreatedAt).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.HasAlternateKey(a => a.OrderIndex);
        }
    }
}

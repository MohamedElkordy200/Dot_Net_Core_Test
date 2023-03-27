using Lean.Domain.DBEntities.Blog;
using Lean.Persistence.Configurations._Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lean.Persistence.Configurations.Blog
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            CommonConfiguration.ConfigureAuditEntity(builder);
            builder.ToTable("Comments");
            builder.HasOne(a => a.Post).WithMany(a => a.Comments).HasForeignKey(a => a.PostId);
        }
    }
}
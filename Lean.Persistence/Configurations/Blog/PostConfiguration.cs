using Lean.Domain.DBEntities.Blog;
using Lean.Persistence.Configurations._Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lean.Persistence.Configurations.Blog
{
    internal class PostConfiguration:IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            CommonConfiguration.ConfigureAuditEntity(builder);
            builder.ToTable("Posts");
            builder.Property(a => a.Title).HasColumnType("nvarchar(250)");

        }
    }
}

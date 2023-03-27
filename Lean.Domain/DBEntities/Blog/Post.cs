using Lean.Domain.DBEntities.Base;

namespace Lean.Domain.DBEntities.Blog
{
    public class Post:AuditEntity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}

using Lean.Domain.DBEntities.Base;

namespace Lean.Domain.DBEntities.Blog
{
    public class Comment: AuditEntity<Guid>
    {
        public string Text { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}

namespace Lean.Contracts.Blog.Dto
{
    public class CommentOutputDto
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public string Text { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}

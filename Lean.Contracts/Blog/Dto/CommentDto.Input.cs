namespace Lean.Contracts.Blog.Dto
{
    public class CommentInputDto
    {
     
        public Guid PostId { get; set; }
        public string Text { get; set; }
    }
    public class CommentUpdateInputDto
    {
        //for Update
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}

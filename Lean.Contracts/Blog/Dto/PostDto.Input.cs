using System.ComponentModel.DataAnnotations;

namespace Lean.Contracts.Blog.Dto
{
    public class PostInputDto
    {
        //for Update
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

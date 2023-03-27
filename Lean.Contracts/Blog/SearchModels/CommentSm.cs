using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lean.Contracts.Blog.SearchModels
{
    public class CommentSm
    {
        public Guid PostId { get; set; }
        public string? Text { get; set; }
        public string? PostDescription { get; set; }
    }
}

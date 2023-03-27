using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lean.Domain.Exceptions.Blog
{
    public  sealed class CommentNotFoundException : NotFoundException
    {
        public CommentNotFoundException():base(RM.Exceptions.CommentNotFound)
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lean.Contracts.Blog.Dto;
using Lean.Contracts.MessageModels;

namespace Lean.Services.Abstractions._Common
{
    public interface IIdSearchable<TOutSearch>
    {
        Task<TOutSearch> GetByIdAsync(Guid id);

    }
}

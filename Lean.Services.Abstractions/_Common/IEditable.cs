using Lean.Contracts.MessageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lean.Services.Abstractions._Common
{
    public interface IEditable<in TEdit>  
    {
        public Task<MessageModel> EditAsync(TEdit model);

    }
}

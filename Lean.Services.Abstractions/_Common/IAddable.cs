using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lean.Contracts.MessageModels;

namespace Lean.Services.Abstractions._Common
{
    public interface IAddable<in TAdd>  
    {
        public Task<MessageModel> AddAsync(TAdd model);
    }
}

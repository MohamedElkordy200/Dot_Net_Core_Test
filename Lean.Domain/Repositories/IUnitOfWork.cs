using Lean.Contracts.MessageModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lean.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task<MessageModel> SaveChangesAsync(CancellationToken cancellationToken = default);
      
    }
}
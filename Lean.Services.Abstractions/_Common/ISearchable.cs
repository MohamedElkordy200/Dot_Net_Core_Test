using Lean.Contracts.MessageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lean.Services.Abstractions._Common
{
    public interface ISearchable<in TSearchModel, TOutSearch>
    {
        public Task<IEnumerable<TOutSearch>> SearchAsync(TSearchModel model, CancellationToken cancellationToken = default);

    }
}
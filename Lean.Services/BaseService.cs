using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lean.Domain.Repositories;
using Lean.Services.Abstractions;

namespace Lean.Services
{
    internal class BaseService
    {
        protected readonly IMapper _mapper;
        protected readonly IRepositoriesManager _repositoryManager;

        public BaseService(IMapper mapper, IRepositoriesManager repositoryManager)
        {
            _mapper=mapper;
            _repositoryManager = repositoryManager;
        }
    }
}

using Microsoft.Extensions.Options;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using Sieve.Models;
using Sieve.Services;
using System.Linq;

namespace QPH_MAIN.Core.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public ISieveProcessor SieveProcessor { get; set; }

        public ApplicationService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public PagedList<Application> GetApplications(SieveModel sieveModel)
        {
            IQueryable<Application> entityFilter = _unitOfWork.ApplicationRepository.GetAllWithReferences();

            var page = sieveModel?.Page ?? 1;
            var pageSize = sieveModel?.PageSize ?? 10;

            if (sieveModel != null)
            {
                // apply pagination in a later step
                entityFilter = SieveProcessor.Apply(sieveModel, entityFilter, applyPagination: true);
            }

            var pagedPosts = PagedList<Application>.CreateFromQuerable(entityFilter, page, pageSize);
            return pagedPosts;
        }
    }
}

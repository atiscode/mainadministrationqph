using Microsoft.Extensions.Options;
using OrderByExtensions;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Core.QueryFilters;
using Sieve.Models;
using Sieve.Services;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public ISieveProcessor SieveProcessor { get; set; }

        public CatalogService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Catalog> GetCatalog(int id) => await _unitOfWork.CatalogRepository.GetById(id);

        public PagedList<Catalog> GetCatalogs(SieveModel sieveModel)
        {
            var entityFilter = _unitOfWork.CatalogRepository.GetAllCatalogs();
            var page = sieveModel?.Page ?? 1;
            var pageSize = sieveModel?.PageSize ?? 10;

            if (sieveModel != null)
            {
                // apply pagination in a later step
                entityFilter = SieveProcessor.Apply(sieveModel, entityFilter, applyPagination: false);
            }
            var pagedPosts = PagedList<Catalog>.CreateFromQuerable(entityFilter, page, pageSize);
            return pagedPosts;
        }

        public async Task InsertCatalog(Catalog views)
        {
            await _unitOfWork.CatalogRepository.Add(views);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteCatalog(int id)
        {
            await _unitOfWork.CatalogRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCatalog(Catalog views)
        {
            var existingCatalog = await _unitOfWork.CatalogRepository.GetById(views.Id);
            existingCatalog.name = views.name;
            existingCatalog.code = views.code;
            _unitOfWork.CatalogRepository.Update(existingCatalog, views);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RebuildHierarchy(CatalogTree tree, int idEnterprise)
        {
            await _unitOfWork.EnterpriseHierarchyCatalogRepository.Add(new EnterpriseHierarchyCatalog { id_enterprise = idEnterprise , children = tree.son, parent = tree.parent });
            if (tree.Children.Count > 0)
            {
                foreach(var sonTree in tree.Children) {
                    await RebuildHierarchy(sonTree, idEnterprise);
                }
            }
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task DeleteHierarchyByEnterpriseId(int enterpriseId) => await _unitOfWork.EnterpriseHierarchyCatalogRepository.RemoveByEntepriseId(enterpriseId);
    }
}
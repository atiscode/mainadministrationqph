using Microsoft.Extensions.Options;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using Sieve.Models;
using Sieve.Services;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class RoleViewPermissionService : IRoleViewPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public ISieveProcessor SieveProcessor { get; set; }


        public RoleViewPermissionService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<RoleViewPermission> GetRoleViewPermissions(SieveModel sieveModel)
        {
            var entityFilter = _unitOfWork.RoleViewPermissionRepository.GetAllWithReferences();
            var page = sieveModel?.Page ?? 1;
            var pageSize = sieveModel?.PageSize ?? 10;

            if (sieveModel != null)
            {
                entityFilter = SieveProcessor.Apply(sieveModel, entityFilter, applyPagination: false);
            }
            var pagedPosts = PagedList<RoleViewPermission>.CreateFromQuerable(entityFilter, page, pageSize);
            return pagedPosts;
        }

        public async Task InsertRoleViewPermission(RoleViewPermission roleViewPermission)
        {
            await _unitOfWork.RoleViewPermissionRepository.Add(roleViewPermission);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteRole(int id)
        {
            await _unitOfWork.RoleViewPermissionRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRoleViewPermission(int id)
        {
            await _unitOfWork.RoleViewPermissionRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}

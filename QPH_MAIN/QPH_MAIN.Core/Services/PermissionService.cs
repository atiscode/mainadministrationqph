using Microsoft.Extensions.Options;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using Sieve.Models;
using Sieve.Services;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public ISieveProcessor SieveProcessor { get; set; }
        public PermissionService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Permission> GetPermission(int id) => await _unitOfWork.PermissionsRepository.GetById(id);

        public PagedList<Permission> GetPermissions(SieveModel sieveModel)
        {
            var entityFilter = _unitOfWork.PermissionsRepository.GetAllPermissions();
            var page = sieveModel?.Page ?? 1;
            var pageSize = sieveModel?.PageSize ?? 10;

            if (sieveModel != null)
            {
                // apply pagination in a later step
                entityFilter = SieveProcessor.Apply(sieveModel, entityFilter, applyPagination: false);
            }
            var pagedPosts = PagedList<Permission>.CreateFromQuerable(entityFilter, page, pageSize);
            return pagedPosts;
        }

        public async Task InsertPermission(Permission permissions)
        {
            await _unitOfWork.PermissionsRepository.Add(permissions);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePermission(Permission permissions)
        {
            var existingPermission = await _unitOfWork.PermissionsRepository.GetById(permissions.Id);
            existingPermission.permission = permissions.permission;
            _unitOfWork.PermissionsRepository.Update(existingPermission);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePermission(int id)
        {
            await _unitOfWork.PermissionsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
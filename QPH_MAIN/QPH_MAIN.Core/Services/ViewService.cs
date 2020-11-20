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
    public class ViewService : IViewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public ISieveProcessor SieveProcessor { get; set; }

        public ViewService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Views> GetView(int id) => await _unitOfWork.ViewRepository.GetById(id);

        public PagedList<Views> GetViews(SieveModel sieveModel)
        {
            var entityFilter = _unitOfWork.ViewRepository.GetAllViews();
            var page = sieveModel?.Page ?? 1;
            var pageSize = sieveModel?.PageSize ?? 10;

            if (sieveModel != null)
            {
                // apply pagination in a later step
                entityFilter = SieveProcessor.Apply(sieveModel, entityFilter, applyPagination: false);
            }
            var pagedPosts = PagedList<Views>.CreateFromQuerable(entityFilter, page, pageSize);
            return pagedPosts;
        }

        public async Task InsertView(Views views)
        {
            await _unitOfWork.ViewRepository.Add(views);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteView(int id)
        {
            await _unitOfWork.ViewRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateView(Views views)
        {
            var existingView = await _unitOfWork.ViewRepository.GetById(views.Id);
            existingView.name = views.name;
            existingView.code = views.code;
            existingView.route = views.route;
            _unitOfWork.ViewRepository.Update(existingView);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RebuildHierarchy(Tree tree, int idUser)
        {
            await _unitOfWork.UserViewRepository.Add(new UserView { userId = idUser , children = tree.son, parent = tree.parent });
            if(tree.cards != null && tree.cards.Count > 0)
            {
                foreach (var card in tree.cards)
                {
                    await _unitOfWork.UserCardGrantedRepository.Add(new UserCardGranted { id_card = card.Id, id_user = idUser });
                    await _unitOfWork.SaveChangesAsync();
                    foreach (var permission in tree.permissions)
                    {
                        if (permission.statuses == 1) await _unitOfWork.UserCardPermissionRepository.Add(new UserCardPermission { id_permission = permission.id, id_card_granted = await _unitOfWork.UserCardGrantedRepository.GetByCardAndUser(card.Id, idUser) });
                    }
                }
            }
            if (tree.Children.Count > 0)
            {
                foreach(var sonTree in tree.Children) {
                    await RebuildHierarchy(sonTree, idUser);
                }
            }
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task DeleteHierarchyByUserId(int userId) => await _unitOfWork.UserViewRepository.RemoveByUserId(userId);
    }
}
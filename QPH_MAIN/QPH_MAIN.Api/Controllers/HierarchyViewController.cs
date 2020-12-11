using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QPH_MAIN.Api.Responses;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.DTOs;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Infrastructure.Interfaces;
using Sieve.Models;
using Sieve.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QPH_MAIN.Api.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class HierarchyViewController : ControllerBase
    {
        private readonly IViewService _viewService;
        private readonly IUserCardGrantedService _userCardGrantedService;
        private readonly IUserCardPermissionService _userCardPermissionService;
        private readonly IUserViewService _hierarchyViewService;
        private readonly ITreeService _treeService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly SieveProcessor _sieveProcessor;

        public HierarchyViewController(ITreeService treeService, IViewService viewService, IUserViewService hierarchyViewService, IUserCardGrantedService userCardGrantedService, IUserCardPermissionService userCardPermissionService, IMapper mapper, IUriService uriService, SieveProcessor sieveProcessor)
        {
            _treeService = treeService;
            _viewService = viewService;
            _userCardGrantedService = userCardGrantedService;
            _userCardPermissionService = userCardPermissionService;
            _hierarchyViewService = hierarchyViewService;
            _mapper = mapper;
            _uriService = uriService;
            _sieveProcessor = sieveProcessor;
        }

        /// <summary>
        /// Retrieve all Views
        /// </summary>
        /// <param name="sieveModel"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpGet("RetrieveViews")]
        public IActionResult GetAllViews(SieveModel sieveModel)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            _viewService.SieveProcessor = _sieveProcessor;
            var entity = _viewService.GetViews(sieveModel);
            var entityDTO = _mapper.Map<IEnumerable<ViewsDto>>(entity);
            var metadata = new Metadata
            {
                TotalCount = entity.TotalCount,
                PageSize = entity.PageSize,
                CurrentPage = entity.CurrentPage,
                TotalPages = entity.TotalPages,
                HasNextPage = entity.HasNextPage,
                HasPreviousPage = entity.HasPreviousPage,
            };
            var response = new ApiResponse<IEnumerable<ViewsDto>>(entityDTO)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        /// <summary>
        /// Retrieve view by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetView(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var views = await _viewService.GetView(id);
            var ViewsDto = _mapper.Map<ViewsDto>(views);
            var response = new ApiResponse<ViewsDto>(ViewsDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new view
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ViewsDto ViewsDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var view = _mapper.Map<View>(ViewsDto);
            await _viewService.InsertView(view);
            ViewsDto = _mapper.Map<ViewsDto>(view);
            var response = new ApiResponse<ViewsDto>(ViewsDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new hierarchy by user and view
        /// </summary>
        [Authorize]
        [HttpPost("rebuildHierarchy")]
        public async Task<IActionResult> Post([FromBody] HierarchyViewNewBuild hierarchyNewBuild)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            string userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            int userIds = int.Parse(userId);
            await _viewService.DeleteHierarchyByUserId(userIds);
            //await _userCardPermissionService.DeletePermissionByUserId(userIds);
            //await _userCardGrantedService.DeleteUserCardGrantedByUserId(userIds);
            foreach (var tree in hierarchyNewBuild.Root)
            {
                var _tree = _mapper.Map<Tree>(tree);
                await _viewService.RebuildHierarchy(_tree, userIds);
            }
            return Ok();
        }

        /// <summary>
        /// Update view
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ViewsDto ViewsDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var view = _mapper.Map<View>(ViewsDto);
            view.Id = ViewsDto.Id;//id;
            var result = await _viewService.UpdateView(view);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Build HierarchyView By User Authenticated.
        /// </summary>
        [Authorize]
        [HttpGet("buildHierarchyViewByUser")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObtainTree()
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            string userName = User.Claims.FirstOrDefault(c => c.Type == "Username").Value;

            return Ok(await _treeService.GetHierarchyTreeByUserId(userName, "Administration", "Dummy"));
        }

        /// <summary>
        /// Remove view by id
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var result = await _viewService.DeleteView(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QPH_MAIN.Api.Responses;
using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.DTOs;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Interfaces;
using Sieve.Models;
using Sieve.Services;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace QPH_MAIN.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RoleViewPermissionController : ControllerBase
    {
        private readonly IRoleViewPermissionService _roleViewPermissionService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly SieveProcessor _sieveProcessor;

        public RoleViewPermissionController(IRoleViewPermissionService roleService, IMapper mapper,
            IUriService uriService, SieveProcessor sieveProcessor)
        {
            _roleViewPermissionService = roleService;
            _mapper = mapper;
            _uriService = uriService;
            _sieveProcessor = sieveProcessor;
        }

        //todo change authorize to switch
        //[Authorize]
        [HttpGet("RetrieveRoles")]
        public IActionResult GetAllRoles(SieveModel sieveModel)
        {
            //if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            _roleViewPermissionService.SieveProcessor = _sieveProcessor;
            PagedList<RoleViewPermission> entity = _roleViewPermissionService.GetRoleViewPermissions(sieveModel);
            var entityDTO = _mapper.Map<IEnumerable<RoleViewPermissionDto>>(entity);
            var metadata = new Metadata
            {
                TotalCount = entity.TotalCount,
                PageSize = entity.PageSize,
                CurrentPage = entity.CurrentPage,
                TotalPages = entity.TotalPages,
                HasNextPage = entity.HasNextPage,
                HasPreviousPage = entity.HasPreviousPage,
            };
            var response = new ApiResponse<IEnumerable<RoleViewPermissionDto>>(entityDTO)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        /// <summary>
        /// Insert new role
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleViewPermissionDto roleViewPermissionDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();

            RoleViewPermission roleViewPermission = _mapper.Map<RoleViewPermission>(roleViewPermissionDto);

            await _roleViewPermissionService.InsertRoleViewPermission(roleViewPermission);

            roleViewPermissionDto = _mapper.Map<RoleViewPermissionDto>(roleViewPermission);

            var response = new ApiResponse<RoleViewPermissionDto>(roleViewPermissionDto);

            return Ok(response);
        }

        /// <summary>
        /// Remove city by id
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var result = await _roleViewPermissionService.DeleteRoleViewPermission(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}

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
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace QPH_MAIN.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _roleService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly SieveProcessor _sieveProcessor;

        public RolesController(IRolesService roleService, IMapper mapper, IUriService uriService, SieveProcessor sieveProcessor)
        {
            _roleService = roleService;
            _mapper = mapper;
            _uriService = uriService;
            _sieveProcessor = sieveProcessor;
        }

        /// <summary>
        /// Retrieve all roles
        /// </summary>
        /// <param name="sieveModel"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpGet("RetrieveRoles")]
        public IActionResult GetAllRoles(SieveModel sieveModel)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            _roleService.SieveProcessor = _sieveProcessor;
            var entity = _roleService.GetRoles(sieveModel);
            var entityDTO = _mapper.Map<IEnumerable<RolesDto>>(entity);
            var metadata = new Metadata
            {
                TotalCount = entity.TotalCount,
                PageSize = entity.PageSize,
                CurrentPage = entity.CurrentPage,
                TotalPages = entity.TotalPages,
                HasNextPage = entity.HasNextPage,
                HasPreviousPage = entity.HasPreviousPage,
            };
            var response = new ApiResponse<IEnumerable<RolesDto>>(entityDTO)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        /// <summary>
        /// Retrieve role by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var role = await _roleService.GetRole(id);
            var roleDto = _mapper.Map<RolesDto>(role);
            var response = new ApiResponse<RolesDto>(roleDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new role
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RolesDto roleDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var role = _mapper.Map<Roles>(roleDto);
            await _roleService.InsertRole(role);
            roleDto = _mapper.Map<RolesDto>(role);
            var response = new ApiResponse<RolesDto>(roleDto);
            return Ok(response);
        }

        /// <summary>
        /// Update role
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RolesDto roleDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var role = _mapper.Map<Roles>(roleDto);
            role.Id = roleDto.Id;//id;
            var result = await _roleService.UpdateRole(role);
            var response = new ApiResponse<bool>(result);
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
            var result = await _roleService.DeleteRole(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}

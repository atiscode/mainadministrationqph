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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace QPH_MAIN.Api.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionsService _permissionsService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly SieveProcessor _sieveProcessor;

        public PermissionsController(IPermissionsService permissionsService, IMapper mapper, IUriService uriService, SieveProcessor sieveProcessor)
        {
            _permissionsService = permissionsService;
            _mapper = mapper;
            _uriService = uriService;
            _sieveProcessor = sieveProcessor;
        }


        /// <summary>
        /// Retrieve all Permissions
        /// </summary>
        /// <param name="sieveModel"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpGet("RetrievePermissions")]
        public IActionResult GetAllPermissions(SieveModel sieveModel)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            _permissionsService.SieveProcessor = _sieveProcessor;
            var entity = _permissionsService.GetPermissions(sieveModel);
            var entityDTO = _mapper.Map<IEnumerable<PermissionsDto>>(entity);
            var metadata = new Metadata
            {
                TotalCount = entity.TotalCount,
                PageSize = entity.PageSize,
                CurrentPage = entity.CurrentPage,
                TotalPages = entity.TotalPages,
                HasNextPage = entity.HasNextPage,
                HasPreviousPage = entity.HasPreviousPage,
            };
            var response = new ApiResponse<IEnumerable<PermissionsDto>>(entityDTO)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        /// <summary>
        /// Retrieve permission by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermission(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var permission = await _permissionsService.GetPermission(id);
            var permissionDto = _mapper.Map<PermissionsDto>(permission);
            var response = new ApiResponse<PermissionsDto>(permissionDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new permission
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PermissionsDto permissionDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var permission = _mapper.Map<Permissions>(permissionDto);
            await _permissionsService.InsertPermission(permission);
            permissionDto = _mapper.Map<PermissionsDto>(permission);
            var response = new ApiResponse<PermissionsDto>(permissionDto);
            return Ok(response);
        }

        /// <summary>
        /// Update permission
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(int id, PermissionsDto permissionDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var permission = _mapper.Map<Permissions>(permissionDto);
            permission.Id = id;
            var result = await _permissionsService.UpdatePermission(permission);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Remove permission by id
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var result = await _permissionsService.DeletePermission(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
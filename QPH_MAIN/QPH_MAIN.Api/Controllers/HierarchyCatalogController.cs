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
    public class HierarchyCatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IEnterpriseHierarchyCatalogService _enterpriseHierarchyCatalogService;
        private readonly ICatalogTreeService _treeService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly SieveProcessor _sieveProcessor;

        public HierarchyCatalogController(ICatalogTreeService treeService, ICatalogService catalogService, IEnterpriseHierarchyCatalogService hierarchyCatalogService, IMapper mapper, IUriService uriService, SieveProcessor sieveProcessor)
        {
            _treeService = treeService;
            _catalogService = catalogService;
            _enterpriseHierarchyCatalogService = hierarchyCatalogService;
            _mapper = mapper;
            _uriService = uriService;
            _sieveProcessor = sieveProcessor;
        }

        /// <summary>
        /// Retrieve all catalogs
        /// </summary>
        /// <param name="sieveModel"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpGet("RetrieveCatalogs")]
        public IActionResult GetAllCatalogs(SieveModel sieveModel)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            _catalogService.SieveProcessor = _sieveProcessor;
            var entity = _catalogService.GetCatalogs(sieveModel);
            var entityDTO = _mapper.Map<IEnumerable<CatalogDto>>(entity);
            var metadata = new Metadata
            {
                TotalCount = entity.TotalCount,
                PageSize = entity.PageSize,
                CurrentPage = entity.CurrentPage,
                TotalPages = entity.TotalPages,
                HasNextPage = entity.HasNextPage,
                HasPreviousPage = entity.HasPreviousPage,
            };
            var response = new ApiResponse<IEnumerable<CatalogDto>>(entityDTO)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        /// <summary>
        /// Retrieve catalog by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalog(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var catalogs = await _catalogService.GetCatalog(id);
            var CatalogDto = _mapper.Map<CatalogDto>(catalogs);
            var response = new ApiResponse<CatalogDto>(CatalogDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new catalog
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CatalogDto CatalogDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var catalog = _mapper.Map<Catalog>(CatalogDto);
            await _catalogService.InsertCatalog(catalog);
            CatalogDto = _mapper.Map<CatalogDto>(catalog);
            var response = new ApiResponse<CatalogDto>(CatalogDto);
            return Ok(response);
        }

        /// <summary>
        /// Insert new hierarchy by user and catalog
        /// </summary>
        [Authorize]
        [HttpPost("rebuildHierarchy/{enterpriseId}")]
        public async Task<IActionResult> Post([FromBody] HierarchyCatalogNewBuild hierarchyNewBuild, int enterpriseId)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            await _catalogService.DeleteHierarchyByEnterpriseId(enterpriseId);
            foreach (var tree in hierarchyNewBuild.Root)
            {
                var _tree = _mapper.Map<CatalogTree>(tree);
                await _catalogService.RebuildHierarchy(_tree, enterpriseId);
            }
            return Ok();
        }

        /// <summary>
        /// Update catalog
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]  CatalogDto CatalogDto)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var catalog = _mapper.Map<Catalog>(CatalogDto);
            catalog.Id = CatalogDto.Id;// id;
            var result = await _catalogService.UpdateCatalog(catalog);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Build HierarchyCatalog By Enterprise Id
        /// </summary>
        [Authorize]
        [HttpGet("buildHierarchyCatalogByEnterprise/{enterpriseId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObtainTree(int enterpriseId)
        {
            return Ok(await _treeService.GetCatalogHierarchyTreeByEnterpriseId(enterpriseId));
        }

        /// <summary>
        /// Build HierarchyCatalog By Catalog Code
        /// </summary>
        [Authorize]
        [HttpGet("buildHierarchyByCodeAndEnterprise/{enterpriseId}/{catalogCode}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObtainTreeByCatalogCodeAndEnterprise(int enterpriseId, string catalogCode)
        {
            return Ok(await _treeService.GetCatalogHierarchyByCode(enterpriseId, catalogCode));
        }

        /// <summary>
        /// Remove catalog by id
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated) throw new AuthenticationException();
            var result = await _catalogService.DeleteCatalog(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
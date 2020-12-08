using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Interfaces;
using Sieve.Models;
using Sieve.Services;
using System.Security.Authentication;

namespace QPH_MAIN.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly ICatalogTreeService _treeService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly SieveProcessor _sieveProcessor;

        public ApplicationController(ICatalogTreeService treeService, IApplicationService applicationService, IMapper mapper, IUriService uriService, SieveProcessor sieveProcessor)
        {
            _treeService = treeService;
            _applicationService = applicationService;
            _mapper = mapper;
            _uriService = uriService;
            _sieveProcessor = sieveProcessor;
        }

        /// <summary>
        /// Retrieve all applications
        /// </summary>
        /// <param name="sieveModel"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpGet("RetrieveCatalogs")]
        public IActionResult GetAllApplications(SieveModel sieveModel)
        {            
            _applicationService.SieveProcessor = _sieveProcessor;
            _applicationService.GetApplications(sieveModel);
            return Ok();
        }
    }
}

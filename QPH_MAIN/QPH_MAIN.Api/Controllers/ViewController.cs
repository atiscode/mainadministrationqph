using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class ViewController : ControllerBase
    {
        private readonly IViewService _viewService;
        private readonly ICatalogTreeService _treeService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly SieveProcessor _sieveProcessor;

        public ViewController(ICatalogTreeService treeService, IViewService applicationService, IMapper mapper, 
            IUriService uriService, SieveProcessor sieveProcessor)
        {
            _treeService = treeService;
            _viewService = applicationService;
            _mapper = mapper;
            _uriService = uriService;
            _sieveProcessor = sieveProcessor;
        }

        /// <summary>
        /// Retrieve all views
        /// </summary>
        /// <param name="sieveModel"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpGet("Retrieve all views")]
        public IActionResult GetAllViews(SieveModel sieveModel)
        {
            _viewService.SieveProcessor = _sieveProcessor;
            _viewService.GetViews(sieveModel);
            return Ok();
        }
    }
}

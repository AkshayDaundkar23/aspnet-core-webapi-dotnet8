using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;


namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext _context;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionController> _logger;

        public RegionController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionController> logger)
        {
            this._context = dbContext;
            this._regionRepository = regionRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        // GET all Regions
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //throw new Exception("Testing Exception");

                // Get data from database - Domain models
                var regionsDomain = await _regionRepository.GetAllAsync();

                _logger.LogInformation($"Data from GetAll Action: {JsonSerializer.Serialize(regionsDomain)}");

                // map Domain model to DTO and return DTO
                return Ok(_mapper.Map<List<RegionDTO>>(regionsDomain));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }           
        }

        // GET Region by Id
        // GET: https://localhost:portnumber/api/regions/{Id}
        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // get region model from db
            var regionDomain = await _regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            // map Domain model to DTO and return DTO
            return Ok(_mapper.Map<RegionDTO>(regionDomain));

        }

        // POST : to create new region
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map Dto to domain model

            var regionDomainModel = _mapper.Map<Region>(addRegionRequestDTO);

            // Use domain model to create Region in db

            await _regionRepository.CreateRegionAsync(regionDomainModel);

            // Map domain model back to dto

            var regionDto = _mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);

        }

        // PUT : to update existing region
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            // map DTO to Domain model
            var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDTO);

            regionDomainModel = await _regionRepository.UpdateRegionAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDTO>(regionDomainModel));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = await _regionRepository.DeleteRegionAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDTO>(regionDomainModel));
        }
    }
}

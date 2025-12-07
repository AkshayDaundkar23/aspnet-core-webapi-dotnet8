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


namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext _context;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this._context = dbContext;
            this._regionRepository = regionRepository;
            this._mapper = mapper;
        }

        // GET all Regions
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get data from database - Domain models
            var regionsDomain = await _regionRepository.GetAllAsync();

            // map Domain model to DTO and return DTO
            return Ok(_mapper.Map<List<RegionDTO>>(regionsDomain));
        }

        // GET Region by Id
        // GET: https://localhost:portnumber/api/regions/{Id}
        [HttpGet]
        [Route("{id:guid}")]
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;


namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext _context;

        public RegionController(NZWalksDbContext dbContext)
        {
            _context = dbContext;
        }

        // GET all Regions
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get data from database - Domain models
            var regionsDomain = _context.Regions.ToList();

            // map Domain model to DTO
            var regionDto = new List<RegionDTO>();

            foreach (var regionDomain in regionsDomain)
            {
                regionDto.Add(new RegionDTO()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            // return DTO
            return Ok(regionDto);
        }

        // GET Region by Id
        // GET: https://localhost:portnumber/api/regions/{Id}
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // get region model from db
            var regionDomain = _context.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            else
            {
                // map Domain model to DTO
                var regionDto = new RegionDTO()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl
                };

                // return DTO
                return Ok(regionDto);
            }
        }

        // POST : to create new region
        [HttpPost]
        public IActionResult CreateRegion([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map Dto to domain model

            var regionDomainModel = new Region
            {
                Name = addRegionRequestDTO.Name,
                Code = addRegionRequestDTO.Code,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };

            // Use domain model to create Region in db

            _context.Regions.Add(regionDomainModel);
            _context.SaveChanges();

            // Map domain model back to dto

            var regionDto = new RegionDTO()
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }


        // PUT : to update existing region
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            // check in region exist
            var regionDomainModel = _context.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            
            // map dto model to Domain model
            regionDomainModel.Name = updateRegionRequestDTO.Name;
            regionDomainModel.Code = updateRegionRequestDTO.Code;
            regionDomainModel.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

            _context.SaveChanges();

            // convert domain to dto

            var regionDomain = new RegionDTO()
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDomain);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = _context.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // delte region
            _context.Regions.Remove(regionDomainModel);
            _context.SaveChanges();

            // if want to return deleted Region --> map domain to Dto

            var regionDto = new RegionDTO()
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}

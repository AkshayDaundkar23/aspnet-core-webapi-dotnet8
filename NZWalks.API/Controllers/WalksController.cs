using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this._mapper = mapper;
            this._walkRepository = walkRepository;
        }

        //create walk
        [HttpPost]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDTO addWalkRequestDto)
        {
            // map dto to domain

            var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDto);

            await _walkRepository.CreateWalkAsync(walkDomainModel);

            // map domain to dto and return

            return Ok(_mapper.Map<WalkDTO>(walkDomainModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        { 
            // get data from db
            var walkDomainModel = await _walkRepository.GetAllWalkAsync();

            // map domain to dto and return

            return Ok(_mapper.Map<List<WalkDTO>>(walkDomainModel));
        }
    }
}

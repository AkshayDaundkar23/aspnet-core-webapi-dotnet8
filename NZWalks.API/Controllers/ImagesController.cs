using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        // Post : /api/Images/UploadImage
        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDTO imageUploadRequestDTO)
        {
            ValidateFileUpload(imageUploadRequestDTO);

            if (ModelState.IsValid)
            {
                // convert DTO to domain

                var imageDomainModel = new Image
                {
                    File = imageUploadRequestDTO.File,
                    FileExtention = Path.GetExtension(imageUploadRequestDTO.File.FileName),
                    FileSizeInBytes = imageUploadRequestDTO.File.Length,
                    FileName = imageUploadRequestDTO.FileName,
                    FileDescription = imageUploadRequestDTO.FileDescription,
                };

                await _imageRepository.UploadImage(imageDomainModel);

                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);
        }

        // private method to validate file
        private void ValidateFileUpload(ImageUploadRequestDTO imageUploadRequestDTO)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtension.Contains(Path.GetExtension(imageUploadRequestDTO.File.FileName).ToLowerInvariant()))
            {
                ModelState.AddModelError("File", "Unsupported File Extenstion");
            }

            if (imageUploadRequestDTO.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size more than 10MB, Please upload file with less than 10MB size");
            }
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be minimun 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be Maximum 3 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be maximun 100 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}

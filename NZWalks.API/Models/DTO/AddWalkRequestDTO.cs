using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequestDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be maximun 100 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Description has to be maximun 100 characters")]
        public string Description { get; set; }

        [Required]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; }
    }
}

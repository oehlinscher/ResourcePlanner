using System.ComponentModel.DataAnnotations;

namespace ResourcePlanner.Api.DTOs
{
    public class UpdateResourceRequestDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public required string Name { get; set; }
    }
}

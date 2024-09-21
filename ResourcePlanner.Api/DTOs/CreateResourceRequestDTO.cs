using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourcePlanner.Api.DTOs
{
    public class CreateResourceRequestDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public required string Name { get; set; }
    }
}

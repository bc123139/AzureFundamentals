using System.ComponentModel.DataAnnotations;

namespace AzureSpookyLogic.Models
{
    public class SpookyRequest
    {
        public string Id { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
    }
}

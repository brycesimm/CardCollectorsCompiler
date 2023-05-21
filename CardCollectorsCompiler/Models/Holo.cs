using System.ComponentModel.DataAnnotations;

namespace CardCollectorsCompiler.Models
{
    public class Holo
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public string? Value { get; set; }
    }
}

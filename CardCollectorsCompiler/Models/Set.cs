using System.ComponentModel.DataAnnotations;

namespace CardCollectorsCompiler.Models
{
    public class Set
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Language { get; set; }
        [Required]
        public int Year { get; set; }
        public int Count { get; set; }
    }
}

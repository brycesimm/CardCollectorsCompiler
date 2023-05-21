using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardCollectorsCompiler.Models
{
    public class Card
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public int? SetId { get; set; }
        [Required]
        public string? Name { get; set; }
        public int? Number { get; set; }
        public string? Rarity { get; set; }
        public string? Edition { get; set; }
        public string? ImageURL { get; set; }
    }
}

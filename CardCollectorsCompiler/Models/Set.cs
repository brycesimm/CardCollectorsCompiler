using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardCollectorsCompiler.Models
{
    public class Set
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Language { get; set; }
        [Required]
        public int Year { get; set; }
        public int Count { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", Name, Language);
            }
        }
    }
}

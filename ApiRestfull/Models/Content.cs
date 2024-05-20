using System.ComponentModel.DataAnnotations;

namespace ApiRestfull.Models
{
    public class Content: BaseEntity
    {
        [Required]
        public string Chapter = string.Empty;

        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = new Course();

    }
}

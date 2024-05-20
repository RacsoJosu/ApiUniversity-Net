using System.ComponentModel.DataAnnotations;

namespace ApiRestfull.Models
{
    enum LevelCourse
    {
        Basic,
        Medium,
        Advanced,
        Expert
    }
    public class Course: BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(300)]
        public string ShortDescription { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;

        private LevelCourse Level { get; set; } = LevelCourse.Basic;

        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        [Required]
        public ICollection<Student> Students { get; set; }  = new List<Student>();

        [Required]
        public Content Content { get; set; } = new Content();

        

    }
}

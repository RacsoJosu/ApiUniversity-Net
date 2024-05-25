using System.ComponentModel.DataAnnotations;

namespace ApiRestfull.Models
{
    public class Student : BaseEntity
    {
        [Required]
        public string accountNumber { get; set; } = string.Empty;
 
        [Required]
        public DateTime DateofBirthday { get; set; }

        public int userId { get; set; }
        public virtual User user { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}

using System.ComponentModel.DataAnnotations;

namespace ApiRestfull.Models
{
    public class User : BaseEntity
    {
        [Required, StringLength(70)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(80)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

}

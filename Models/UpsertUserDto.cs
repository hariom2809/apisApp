using System.ComponentModel.DataAnnotations;

namespace apisApp.Models
{
    public class UpsertUserDto
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }
}

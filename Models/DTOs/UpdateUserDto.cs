namespace apisApp.Models.DTOs
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

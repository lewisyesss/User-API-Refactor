using System.ComponentModel.DataAnnotations;

namespace Tests.Users.Api.DTO
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Age { get; set; }
    }
}

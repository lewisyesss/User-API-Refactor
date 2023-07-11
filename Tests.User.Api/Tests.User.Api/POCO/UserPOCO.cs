using System.ComponentModel.DataAnnotations;
using Tests.User.Api.Models;

namespace Tests.User.Api.POCO
{
    public class UserPOCO
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }

        public UserModel ToModel()
        {
            return new UserModel
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Age = this.Age
            };

        }
    }
}

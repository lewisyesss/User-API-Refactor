using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
/// Added the library in order to make a parameter be required
namespace Tests.User.Api.Models
{
    [Table("users")]
    public class User
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

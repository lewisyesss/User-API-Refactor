using System.ComponentModel.DataAnnotations.Schema;

namespace Tests.User.Api.Models
{
    [Table("users")]
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace Tests.User.Api.Models
{
    [Table("users")]
    public class User
    {
        public int Id { get; set; }
       
        // Validation for first name, last name and age - a user is required to have these
        [Required("First name is required")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        // Age is changed to an integer data type
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Tests.Users.Api.Models
{
    [Table("users")]
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
    }
}

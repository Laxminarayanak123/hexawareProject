using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace weHelpTax_.net6_.Model
{
    public partial class Users
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}

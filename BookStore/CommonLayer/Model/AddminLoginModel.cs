using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class AddminLoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string Token { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class UserRegModel
    {
        [Required(ErrorMessage = "{0} should not be empty")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        public long MobileNumber { get; set; }

    }
}

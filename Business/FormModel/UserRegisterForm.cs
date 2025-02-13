//UserRegisterForm.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.FormModel
{
    public class UserRegisterForm
    {
        [Required, MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required, MinLength(8)]
        public string? Password { get; set; }


        //public int RoleId { get; set; }  // Add this for role selection
    }

}
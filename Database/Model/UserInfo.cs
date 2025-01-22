//UserInfo.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class UserInfo
    {
        [Key]
        [MaxLength(128)]
        public string UserInfoId { get; set; } = Guid.NewGuid().ToString(); // Primary key

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } // Non-nullable with max length

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; } // Non-nullable, email validation

        [Required]
        public string PasswordHash { get; set; } // Non-nullable, hashed passwords only

        [MaxLength(50)]
        public string Location { get; set; } // Optional field

        public bool IsActive { get; set; } = true; // Default to active

        [Required]
        public int RoleId { get; set; } // Foreign key for Role table
    }
}

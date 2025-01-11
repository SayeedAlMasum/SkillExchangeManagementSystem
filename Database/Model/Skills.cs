using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Skills : UserInfo
    {
        [Required]
        public int SkillId { get; set; }
        [Required]
        public string? SkillName { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        public string? ProficiencyLevel { get; set; }
    }
}
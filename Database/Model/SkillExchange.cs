using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class SkillExchange:UserInfo
    {
        [Key]
        public int ExchangeId { get; set; }
        [Required]
        public int SkillOfferedId { get; set; }
        [Required]
        public int SkillRequestedId { get; set; }
        [Required]
        public string? SkillStatus { get; set; }

    }
}

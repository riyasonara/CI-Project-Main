using CI_PLatform_Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Project.Models
{
    public class AdminSkillViewModel
    {
        public List<Skill> skills { get; set; }

        public long SkillId { get; set; }

        [Required(ErrorMessage ="Skill name required")]
        public string SkillName { get; set; }

        [Required(ErrorMessage ="Status is required")]
        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace FormBuilder.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int TemplateId { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}

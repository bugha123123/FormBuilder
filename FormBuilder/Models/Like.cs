using FormBuilder.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormBuilder.Models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }

        // The user who liked
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } 

        public int? CommentId { get; set; }

        [ForeignKey(nameof(CommentId))]
        public virtual Comment Comment { get; set; }

        public int? TemplateId { get; set; }

        [ForeignKey(nameof(TemplateId))]
        public virtual FormTemplate Template { get; set; }

        public DateTime LikedAt { get; set; } = DateTime.UtcNow;

        public LikeTargetType LikeTargetType { get; set; }
    }
}

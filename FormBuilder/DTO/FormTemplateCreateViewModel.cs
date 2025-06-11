using FormBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace FormBuilder.DTO
{
    public class FormTemplateCreateViewModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; } // markdown input

        [Required]
        public int TopicId { get; set; }  // selected topic from DB list

        public IFormFile? ImageFile { get; set; } // for Azure upload

        public bool IsPublic { get; set; }

        public List<string> Tags { get; set; } = new();

        public List<UserAssignmentViewModel> AssignedUsers { get; set; } = new();

        public List<QuestionViewModel> Questions { get; set; } = new();
    }




}

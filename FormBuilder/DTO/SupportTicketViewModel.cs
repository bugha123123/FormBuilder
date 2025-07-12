using System.ComponentModel.DataAnnotations;

namespace FormBuilder.Models
{
    public class SupportTicketViewModel
    {
        [Required(ErrorMessage = "Summary is required.")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Priority is required.")]
        public string Priority { get; set; }

        public string PageUrl { get; set; }

        public FormTemplate FormTemplate { get; set; } 
    }
}

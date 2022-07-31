using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required, StringLength(255), Display(Name = "Driving License")]
        public string DrivingLicense { get; set; }

        [Required, StringLength(50)]
        public string PhoneNumber { get; set; }
    }
}
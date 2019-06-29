using System.ComponentModel.DataAnnotations;

namespace SimpleERP.Models.API.User
{
    public class ChangeActivationModel
    {
        public string Email { get; set; }

        [Required]
        public bool? IsActive { get; set; }
    }
}

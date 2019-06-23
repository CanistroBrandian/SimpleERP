using SimpleERP.Models.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.ViewModels
{
    public class RegisterViewModel : IValidatableObject
    {
        [Required]
        [Display(Name = "Имя")]
        public string NameFirst { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string NameLast { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Адрес")]
        public string Adress { get; set; }

        [Required]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

        [Required]
        [Display(Name = "Тип")]
        public string Type { get; set; }

        public int? DepartmentId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!new List<string> { nameof(Employe), nameof(Manager), nameof(Client) }.Any(s => s == Type))
            {
                yield return new ValidationResult("Выберите тип регистрации из списка", new[] { nameof(Type) });
            }
            else if (new List<string> { nameof(Employe), nameof(Manager) }.Any(s => s == Type) && !DepartmentId.HasValue)
            {
                yield return new ValidationResult($"Если вы {nameof(Manager)} или {nameof(Employe)}, вы должны выбрать {nameof(DepartmentId)}", new[] { nameof(Type) });
            }
        }

    }
}

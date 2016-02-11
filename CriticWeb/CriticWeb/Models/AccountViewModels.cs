using CriticWeb.Annotations;
using CriticWeb.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CriticWeb.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Адреса електронної пошти")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Код")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Запам'ятати браузер?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Адреса електронної пошти")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Адреса електронної пошти")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати мене")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Нікнейм не може бути порожнім")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Довжина нікнейму повинна бути від 3 до 64 символів")]
        [Display(Name = "Нікнейм")]
        [UserName(ErrorMessage = "Такий нікнейм уже існує")]
        public string Username { get; set; }

        [StringLength(256, ErrorMessage = "Довжина ім'я повинна бути не більше 256 символів")]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        [StringLength(256, ErrorMessage = "Довжина прізвища повинна бути не більше 256 символів")]
        [Display(Name = "Прізвище")]
        public string Surname { get; set; }

        [Display(Name = "Дата народження")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Стать")]
        public UserCritic.GenderType? Gender { get; set; }

        [StringLength(256, ErrorMessage = "Довжина назви країни повинна бути не більше 256 символів")]
        [Display(Name = "Країна")]
        public string Country { get; set; }

        [StringLength(256, ErrorMessage = "Довжина назви ЗМІ повинна бути не більше 256 символів")]
        [Display(Name = "ЗМІ (де працюєте)")]
        public string PublicationCompany { get; set; }

        [Display(Name = "Зображення")]
        public byte[] Image { get; set; }

        [Required(ErrorMessage = "Адреса електронної пошти не може бути порожньою")]
        [EmailAddress]
        [Display(Name = "Адреса електронної пошти")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль не може бути порожнім")]
        [StringLength(100, ErrorMessage = "Значення {0} повинно мати не менше {2} символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження пароля")]
        [Compare("Password", ErrorMessage = "Пароль та його підтвердження не співпадають.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Адреса електронної пошти")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значення {0} повинно мати не менше {2} символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження пароля")]
        [Compare("Password", ErrorMessage = "Пароль та його підтвердження не співпадають.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Пошта")]
        public string Email { get; set; }
    }
}

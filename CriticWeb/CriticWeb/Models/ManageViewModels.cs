using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using CriticWeb.Annotations;
using System;
using CriticWeb.DataLayer;

namespace CriticWeb.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }

        [Required(ErrorMessage = "Нікнейм не може бути порожнім")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Довжина нікнейму повинна бути від 3 до 64 символів")]
        [Display(Name = "Нікнейм")]
        [UserNameEdit(ErrorMessage = "Такий нікнейм уже існує")]
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

        ////[Required(ErrorMessage = "Адреса електронної пошти не може бути порожньою")]
        ////[EmailAddress]
        ////[Display(Name = "Адреса електронної пошти")]
        ////public string Email { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Значення {0} повинно мати символів не менше: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новий пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження нового пароля")]
        [Compare("NewPassword", ErrorMessage = "Новий пароль та його підтвердження не співпадають.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Поле з поточним паролем не може бути порожнім")]
        [DataType(DataType.Password)]
        [Display(Name = "Поточний пароль")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Поле з новим паролем не може бути порожнім")]
        [StringLength(100, ErrorMessage = "Значення {0} повинно мати символів не менше: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новий пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження нового пароля")]
        [Compare("NewPassword", ErrorMessage = "Новий пароль та його підтвердження не співпадають.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Номер телефона")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Код")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}
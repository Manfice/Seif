using System.ComponentModel.DataAnnotations;
using Seif.Models.SeifData;

namespace Seif.ViewModels
{
    public class CartViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Укажите ваш адрес электронной почты.")]
        [Display(Name = "E-mail АДРЕС")]
        [EmailAddress]
        public string EMail { get; set; }
        [Required(ErrorMessage = "Укажите ваш контактный телефон!")]
        [Display(Name = "ТЕЛЕФОН ДЛЯ СВЯЗИ")]
        [Phone]
        public string Phone { get; set; }
    }
}
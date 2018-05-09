using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyunMVC.Data.Model
{
    public class Mail
    {
        [MaxLength(150, ErrorMessage = "150 karakteri aşmayınız")]
        [Required]
        public string Ad { get; set; }

        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$", ErrorMessage = "Mail adresinizi doğru giriniz.")]
        [Required]
        public string MailAdresi { get; set; }

        [MaxLength(150, ErrorMessage = "150 karakteri aşmayınız")]
        [Required]
        public string Konu { get; set; }

        [Required]
        public string Mesaj { get; set; }
    }
}

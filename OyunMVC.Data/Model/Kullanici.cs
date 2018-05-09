using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyunMVC.Data.Model
{
    [Table("Kullanici")]
    public class Kullanici : BaseEntity
    {
        public int RolID { get; set; }

        [Display(Name="Ad Soyad")]
        [MaxLength(150, ErrorMessage="150 karakteri aşmayınız")]
        [Required]
        public string AdSoyad { get; set; }

        [Display(Name="Email")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$", ErrorMessage = "Mail adresinizi doğru giriniz.")]
        [Required]
        public string Email { get; set; }

        [Display(Name="Şifre")]
        [DataType(DataType.Password)]
        [MaxLength(16, ErrorMessage="16 karakteri aşmayınız")]
        [Required]
        public string Sifre { get; set; }

        public virtual Rol Rol { get; set; }

        public ICollection<Haber> Haberler { get; set; }

        public ICollection<Yorum> KullaniciYorumlar { get; set; }
    }
}

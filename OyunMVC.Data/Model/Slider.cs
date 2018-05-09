using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyunMVC.Data.Model
{
    [Table("Slider")]
    public class Slider : BaseEntity
    {
        [Display(Name = "Başlık")]
        [MinLength(3, ErrorMessage = "3 karakterden fazla giriniz"), MaxLength(255, ErrorMessage = "255 karakteri aşmayınız")]
        public string Baslik { get; set; }

        [Display(Name = "Açıklama")]
        [MinLength(3, ErrorMessage = "3 karakterden fazla giriniz"), MaxLength(255, ErrorMessage = "255 karakteri aşmayınız")]
        public string Aciklama { get; set; }

        [Display(Name = "Resim Yolu")]
        [MinLength(3, ErrorMessage = "3 karakterden fazla giriniz"), MaxLength(255, ErrorMessage = "255 karakteri aşmayınız")]
        public string ResimYolu { get; set; }

        public int HaberID { get; set; }

        public virtual Haber Haber { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyunMVC.Data.Model
{
    [Table("Haber")]
    public class Haber : BaseEntity
    {
        public int KategoriID { get; set; }

        public int KullaniciID { get; set; }

        [Display(Name="Haber Başlık")]
        [MaxLength(250, ErrorMessage="250 karakterden az giriniz")]
        [Required]
        public string Baslik { get; set; }

        [Display(Name="Kısa Açıklama")]        
        public string KisaAciklama { get; set; }

        [Display(Name="Açıklama")]       
        public string Aciklama { get; set; }

        public int Okunma { get; set; }

        public string Resim { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<Resim> Resimler { get; set; }

        public virtual ICollection<Etiket> Etiketler { get; set; }

        public ICollection<Yorum> HaberYorumlar { get; set; }
    }
}

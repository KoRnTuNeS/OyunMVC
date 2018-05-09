using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyunMVC.Data.Model
{
    [Table("Rol")]
    public class Rol : BaseEntity
    {
        [Display(Name="Rol Adı")]
        [MinLength(3, ErrorMessage="3 karakterden az girdiniz"), MaxLength(150, ErrorMessage="150 karakterden çok girdiniz")]
        public string RolAdi { get; set; }

        public List<Kullanici> Kullanicilar { get; set; }
    }
}

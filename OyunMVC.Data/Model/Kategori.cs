using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyunMVC.Data.Model
{
    [Table("Kategori")]
    public class Kategori : BaseEntity
    {
        [MinLength(2, ErrorMessage = "2 karakterden az girdiniz."), MaxLength(150, ErrorMessage = "150 karakterden çok girdiniz")]
        [Required]
        public string KategoriAdi { get; set; }

        public int ParentID { get; set; }

        public List<Haber> Haberler { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyunMVC.Data.Model
{
    [Table("Resim")]
    public class Resim : BaseEntity
    {
        public int HaberID { get; set; }

        public string ResimYolu { get; set; }

        public virtual Haber Haber { get; set; }
    }
}

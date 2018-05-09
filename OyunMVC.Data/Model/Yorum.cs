using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyunMVC.Data.Model
{
    [Table("Yorum")]
    public class Yorum : BaseEntity
    {


        public int HaberID { get; set; }

        public int KullaniciID { get; set; }

        public string GelenYorum { get; set; }

        public virtual Haber Haber { get; set; }

        public virtual Kullanici Kullanici { get; set; }
    }
}

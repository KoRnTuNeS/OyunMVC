using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyunMVC.Data.Model
{
    [Table("Etiket")]
    public class Etiket : BaseEntity
    {
        public string EtiketAdi { get; set; }

        public virtual List<Haber> Haberler { get; set; }
    }
}

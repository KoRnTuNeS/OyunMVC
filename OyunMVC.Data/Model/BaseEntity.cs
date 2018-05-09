using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyunMVC.Data.Model
{
    public class BaseEntity
    {
        public int ID { get; set; }
        
        private DateTime Tarih = DateTime.Now;
        private bool Aktif = true;

        public DateTime EklenmeTarihi { 
            get {
                return Tarih;
            }
            set {
                Tarih = value;
            }
        }

        public bool AktifMi {
            get {
                return Aktif;
            }
            set {
                Aktif = value;
            }
        }
    }
}

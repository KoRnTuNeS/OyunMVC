using OyunMVC.Core.Infrastructure;
using OyunMVC.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using OyunMVC.Data.Model;

namespace OyunMVC.Core.Repository
{
    public class EtiketRepository : IEtiketRepository
    {
        OyunContext db = new OyunContext();
        public IEnumerable<Data.Model.Etiket> GetAll()
        {
            return db.Etiket.Select(x => x);
        }

        public Data.Model.Etiket GetByID(int id)
        {
            return db.Etiket.FirstOrDefault(x => x.ID == id);
        }

        public Data.Model.Etiket Get(System.Linq.Expressions.Expression<Func<Data.Model.Etiket, bool>> expression)
        {
            return db.Etiket.FirstOrDefault(expression);
        }

        public IQueryable<Data.Model.Etiket> GetMany(System.Linq.Expressions.Expression<Func<Data.Model.Etiket, bool>> expression)
        {
            return db.Etiket.Where(expression);
        }

        public void Insert(Data.Model.Etiket obj)
        {
            db.Etiket.Add(obj);
        }

        public void Update(Data.Model.Etiket obj)
        {
            db.Etiket.AddOrUpdate(obj);
        }

        public void Delete(int id)
        {
            var etiket = GetByID(id);
            if (etiket != null)
            {
                db.Etiket.Remove(etiket);
            }
        }

        public int Count()
        {
            return db.Etiket.Count();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IQueryable<Data.Model.Etiket> Etiketler(string[] etiketler)
        {
            return db.Etiket.Where(x => etiketler.Contains(x.EtiketAdi));
        }
        public void EtiketEkle(int HaberID, string etiket)
        {
            if (etiket != null || etiket != "")
            {
                string[] Etiketler = etiket.Split(',');
                foreach (var item in Etiketler)
                {
                    Etiket e = this.Get(x => x.EtiketAdi.ToLower() == item.ToLower().Trim());
                    if (e == null)
                    {
                        e = new Etiket();
                        e.EtiketAdi = item;
                        this.Insert(e);
                        this.Save();
                    }
                }
                this.HaberEtiketEkle(HaberID, Etiketler);
            }
        }

        public void HaberEtiketEkle(int HaberID, string[] etiketler)
        {
            var haber = db.Haber.FirstOrDefault(x => x.ID == HaberID);
            var gelenEtiket = this.Etiketler(etiketler);

            haber.Etiketler.Clear();

            gelenEtiket.ToList().ForEach(etiket => haber.Etiketler.Add(etiket));
            db.SaveChanges();
        }
    }
}

using OyunMVC.Core.Infrastructure;
using OyunMVC.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace OyunMVC.Core.Repository
{
    public class KullaniciRepository : IKullaniciRepository
    {
        OyunContext db = new OyunContext();
        public IEnumerable<Data.Model.Kullanici> GetAll()
        {
            return db.Kullanici.Select(x => x);
        }

        public Data.Model.Kullanici GetByID(int id)
        {
            return db.Kullanici.FirstOrDefault(x => x.ID == id);
        }

        public Data.Model.Kullanici Get(System.Linq.Expressions.Expression<Func<Data.Model.Kullanici, bool>> expression)
        {
            return db.Kullanici.FirstOrDefault(expression);
        }

        public IQueryable<Data.Model.Kullanici> GetMany(System.Linq.Expressions.Expression<Func<Data.Model.Kullanici, bool>> expression)
        {
            return db.Kullanici.Where(expression);
        }

        public void Insert(Data.Model.Kullanici obj)
        {
            db.Kullanici.Add(obj);
        }

        public void Update(Data.Model.Kullanici obj)
        {
            db.Kullanici.AddOrUpdate(obj);
        }

        public void Delete(int id)
        {
            var kullanici = GetByID(id);
            if (kullanici != null)
            {
                db.Kullanici.Remove(kullanici);
            }
        }

        public int Count()
        {
            return db.Kullanici.Count();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}

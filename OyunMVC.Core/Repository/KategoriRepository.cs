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
    public class KategoriRepository : IKategoriRepository
    {
        OyunContext db = new OyunContext();
        public IEnumerable<Data.Model.Kategori> GetAll()
        {
            return db.Kategori.Select(x => x);
        }

        public Data.Model.Kategori GetByID(int id)
        {
            return db.Kategori.FirstOrDefault(x => x.ID == id);
        }

        public Data.Model.Kategori Get(System.Linq.Expressions.Expression<Func<Data.Model.Kategori, bool>> expression)
        {
            return db.Kategori.FirstOrDefault(expression);
        }

        public IQueryable<Data.Model.Kategori> GetMany(System.Linq.Expressions.Expression<Func<Data.Model.Kategori, bool>> expression)
        {
            return db.Kategori.Where(expression);
        }

        public void Insert(Data.Model.Kategori obj)
        {
            db.Kategori.Add(obj);
        }

        public void Update(Data.Model.Kategori obj)
        {
            db.Kategori.AddOrUpdate(obj);
        }

        public void Delete(int id)
        {
            var kategori = GetByID(id);
            if (kategori != null)
            {
                db.Kategori.Remove(kategori);
            }
        }

        public int Count()
        {
            return db.Kategori.Count();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}

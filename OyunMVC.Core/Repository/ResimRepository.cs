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
    public class ResimRepository : IResimRepository
    {
        OyunContext db = new OyunContext();
        public IEnumerable<Data.Model.Resim> GetAll()
        {
            return db.Resim.Select(x => x);
        }

        public Data.Model.Resim GetByID(int id)
        {
            return db.Resim.FirstOrDefault(x => x.ID == id);
        }

        public Data.Model.Resim Get(System.Linq.Expressions.Expression<Func<Data.Model.Resim, bool>> expression)
        {
            return db.Resim.FirstOrDefault(expression);
        }

        public IQueryable<Data.Model.Resim> GetMany(System.Linq.Expressions.Expression<Func<Data.Model.Resim, bool>> expression)
        {
            return db.Resim.Where(expression);
        }

        public void Insert(Data.Model.Resim obj)
        {
            db.Resim.Add(obj);
        }

        public void Update(Data.Model.Resim obj)
        {
            db.Resim.AddOrUpdate(obj);
        }

        public void Delete(int id)
        {
            var resim = GetByID(id);
            if (resim != null)
            {
                db.Resim.Remove(resim);
            }
        }

        public int Count()
        {
            return db.Resim.Count();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}

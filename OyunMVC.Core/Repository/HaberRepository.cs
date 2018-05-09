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
    public class HaberRepository : IHaberRepository
    {
        OyunContext db = new OyunContext();
        public IEnumerable<Data.Model.Haber> GetAll()
        {
            return db.Haber.Select(x => x);
        }

        public Data.Model.Haber GetByID(int id)
        {
            return db.Haber.FirstOrDefault(x => x.ID == id);
        }

        public Data.Model.Haber Get(System.Linq.Expressions.Expression<Func<Data.Model.Haber, bool>> expression)
        {
            return db.Haber.FirstOrDefault(expression);
        }

        public IQueryable<Data.Model.Haber> GetMany(System.Linq.Expressions.Expression<Func<Data.Model.Haber, bool>> expression)
        {
            return db.Haber.Where(expression);
        }

        public void Insert(Data.Model.Haber obj)
        {
            db.Haber.Add(obj);
        }

        public void Update(Data.Model.Haber obj)
        {
            db.Haber.AddOrUpdate(obj);
        }

        public void Delete(int id)
        {
            var haber = GetByID(id);
            if (haber != null)
            {
                db.Haber.Remove(haber);
            }
        }

        public int Count()
        {
            return db.Haber.Count();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}

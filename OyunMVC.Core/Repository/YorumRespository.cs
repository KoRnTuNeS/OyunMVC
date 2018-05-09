using OyunMVC.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyunMVC.Data.Model;
using System.Linq.Expressions;
using OyunMVC.Data.DataContext;
using System.Data.Entity.Migrations;

namespace OyunMVC.Core.Repository
{    
    public class YorumRespository : IYorumRepository
    {
        OyunContext db = new OyunContext();
        public int Count()
        {
            return db.Yorum.Count();
        }

        public void Delete(int id)
        {
            var yorum = GetByID(id);
            if (yorum != null)
            {
                db.Yorum.Remove(yorum);
            }
        }

        public Yorum Get(Expression<Func<Yorum, bool>> expression)
        {
            return db.Yorum.FirstOrDefault(expression);
        }

        public IEnumerable<Yorum> GetAll()
        {
            return db.Yorum.Select(x => x);
        }

        public Yorum GetByID(int id)
        {
            return db.Yorum.FirstOrDefault(x => x.ID == id);
        }

        public IQueryable<Yorum> GetMany(Expression<Func<Yorum, bool>> expression)
        {
            return db.Yorum.Where(expression);
        }

        public int HaberYorumSayisi(int id)
        {
            return db.Yorum.Where(x => x.HaberID == id).Count();
        }

        public void Insert(Yorum obj)
        {
            db.Yorum.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Yorum obj)
        {
            db.Yorum.AddOrUpdate(obj);
        }
    }
}

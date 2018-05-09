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
    public class SliderRepository : ISliderRepository
    {
        OyunContext db = new OyunContext();
        public int Count()
        {
            return db.Slider.Count();
        }

        public void Delete(int id)
        {
            Slider s = GetByID(id);
            if (s != null)
            {
                db.Slider.Remove(s);
            }
        }

        public Slider Get(Expression<Func<Slider, bool>> expression)
        {
            return db.Slider.FirstOrDefault(expression);
        }

        public IEnumerable<Slider> GetAll()
        {
            return db.Slider.Select(x => x);
        }

        public Slider GetByID(int id)
        {
            return db.Slider.FirstOrDefault(x => x.ID == id);
        }

        public IQueryable<Slider> GetMany(Expression<Func<Slider, bool>> expression)
        {
            return db.Slider.Where(expression);
        }

        public void Insert(Slider obj)
        {
            db.Slider.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Slider obj)
        {
            db.Slider.AddOrUpdate(obj);
        }
    }
}

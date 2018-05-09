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
    public class RolRepository : IRolRepository
    {
        OyunContext db = new OyunContext();
        public IEnumerable<Data.Model.Rol> GetAll()
        {
            return db.Rol.Select(x => x);
        }

        public Data.Model.Rol GetByID(int id)
        {
            return db.Rol.FirstOrDefault(x => x.ID == id);
        }

        public Data.Model.Rol Get(System.Linq.Expressions.Expression<Func<Data.Model.Rol, bool>> expression)
        {
            return db.Rol.FirstOrDefault(expression);
        }

        public IQueryable<Data.Model.Rol> GetMany(System.Linq.Expressions.Expression<Func<Data.Model.Rol, bool>> expression)
        {
            return db.Rol.Where(expression);
        }

        public void Insert(Data.Model.Rol obj)
        {
            db.Rol.Add(obj);
        }

        public void Update(Data.Model.Rol obj)
        {
            db.Rol.AddOrUpdate(obj);
        }

        public void Delete(int id)
        {
            var rol = GetByID(id);
            if (rol != null)
            {
                db.Rol.Remove(rol);
            }
        }

        public int Count()
        {
            return db.Rol.Count();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}

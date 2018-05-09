using OyunMVC.Core.Infrastructure;
using OyunMVC.Data.Model;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OyunMVC.WebUI.Controllers
{
    public class KategoriController : Controller
    {
        #region Repository
        private readonly IKategoriRepository _kategoriRepository;
        private readonly IHaberRepository _haberRepository;
        public KategoriController(IKategoriRepository kategoriRepository, IHaberRepository haberRepository)
        {
            _kategoriRepository = kategoriRepository;
            _haberRepository = haberRepository;
        }
        // GET: Kategori
        #endregion

        #region Kategori Liste
        public ActionResult Liste(int id, int sayfa = 1)
        {
            ViewBag.Kategoriler = _kategoriRepository.GetAll();
            Kategori k = _kategoriRepository.GetByID(id);
            var haberListe = _haberRepository.GetMany(x => x.KategoriID == k.ID);
            string kAdi = k.KategoriAdi;
            ViewBag.KategoriAdi = kAdi;
            return View(haberListe.OrderByDescending(x => x.EklenmeTarihi).ToPagedList(sayfa, 3));
        }
        #endregion
    }
}
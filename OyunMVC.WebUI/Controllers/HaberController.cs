using OyunMVC.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace OyunMVC.WebUI.Controllers
{
    public class HaberController : Controller
    {
        #region Repository
        private readonly IHaberRepository _haberRepository;
        private readonly IKategoriRepository _kategoriRepository;
        private readonly IYorumRepository _yorumRepository;
        public HaberController(IHaberRepository haberRepository, IKategoriRepository kategoriRepository, IYorumRepository yorumRepository)
        {
            _haberRepository = haberRepository;
            _kategoriRepository = kategoriRepository;
            _yorumRepository = yorumRepository;
        }
        // GET: Haber
        #endregion

        #region Haber İçerik
        public ActionResult Icerik(int id, int sayfa = 1)
        {
            ViewBag.Kategoriler = _kategoriRepository.GetAll();
            //ViewBag.Yorumlar = _yorumRepository.GetMany(x => x.HaberID == id);
            var YorumListe = _yorumRepository.GetMany(x => x.HaberID == id);
            ViewBag.YorumSayisi = _yorumRepository.HaberYorumSayisi(id).ToString();
            var haber = _haberRepository.GetByID(id);
            ViewBag.Haber = haber;
            OkunmaSayisiArttir(id);
            //return View(haber);
            return View(YorumListe.OrderBy(x => x.EklenmeTarihi).ToPagedList(sayfa, 5));
        }
        #endregion

        #region Haber Listele
        [HttpGet]
        public ActionResult Listele(int sayfa = 1, string arama = "")
        {
            ViewBag.Kategoriler = _kategoriRepository.GetAll();
            var haberListesi = _haberRepository.GetAll();
            ViewBag.Arama = arama;
            return View(haberListesi.OrderByDescending(x => x.EklenmeTarihi).Where(x => x.Baslik.ToLower().Contains(arama.ToLower())).ToPagedList(sayfa, 3));
        }
        #endregion

        #region Metod OkunmaSayisiArttir
        public void OkunmaSayisiArttir(int id)
        {
            var h = _haberRepository.GetByID(id);
            h.Okunma += 1;
            _haberRepository.Save();
        }
        #endregion
    }
}
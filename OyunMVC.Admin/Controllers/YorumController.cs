using OyunMVC.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using OyunMVC.Data.Model;
using OyunMVC.Admin.Class;
using OyunMVC.Admin.CustomFilter;

namespace OyunMVC.Admin.Controllers
{
    public class YorumController : Controller
    {
        #region Repository
        private readonly IYorumRepository _yorumRepository;
        private readonly IHaberRepository _haberRepository;
        public YorumController(IYorumRepository yorumRepository, IHaberRepository haberRepository)
        {
            _yorumRepository = yorumRepository;
            _haberRepository = haberRepository;
        }
        // GET: Yorum
        #endregion

        #region Index
        [LoginFilter]
        public ActionResult Index(int sayfa = 1)
        {
            var yorumListe = _yorumRepository.GetAll();
            return View(yorumListe.OrderByDescending(x => x.ID).ToPagedList(sayfa, 3));
        }
        #endregion

        #region Yorum Sil
        [LoginFilter]
        public JsonResult Sil(int id)
        {
            Yorum y = _yorumRepository.GetByID(id);
            if (y == null)
            {
                throw new Exception("Yorum bulunamadı");
            }
            try
            {
                _yorumRepository.Delete(y.ID);
                _yorumRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Yorum başarıyla silindi" });
            }
            catch (Exception ex)
            {
                return Json(new ResultJson { Success = false, Message = "Bir hata oluştu" });
            }
        }
        #endregion

        #region Yorum Düzenle
        [LoginFilter]
        public ActionResult Duzenle(int id)
        {
            Yorum y = _yorumRepository.GetByID(id);
            if (y == null)
            {
                throw new Exception("Yorum bulunamadı");
            }
            if (Session["Adres"] == null)
            {
                Session["Adres"] = Request.UrlReferrer.ToString();
            }
            else
            {
                Session["Adres"] = null;
                Session["Adres"] = Request.UrlReferrer.ToString();
            }
            return View(y);
        }
        [HttpPost]
        public JsonResult Duzenle(Yorum yorum)
        {
            try
            {
                Yorum y = _yorumRepository.GetByID(yorum.ID);
                y.GelenYorum = yorum.GelenYorum;
                y.AktifMi = yorum.AktifMi;
                _yorumRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Yorum başarıyla düzenlendi" });
            }
            catch (Exception)
            {
                return Json(new ResultJson { Success = false, Message = "Bir hata oluştu" });
            }
        }
        #endregion

        #region Haberin Yorumları
        public ActionResult HaberYorumlari(int id, int sayfa = 1)
        {
            var haberYorumlari = _yorumRepository.GetMany(x => x.HaberID == id);
            return View(haberYorumlari.OrderByDescending(x => x.EklenmeTarihi).ToPagedList(sayfa, 3));
        }
        #endregion
    }
}

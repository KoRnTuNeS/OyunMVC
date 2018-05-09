using OyunMVC.Admin.Class;
using OyunMVC.Core.Infrastructure;
using OyunMVC.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using OyunMVC.Admin.CustomFilter;

namespace OyunMVC.Admin.Controllers
{
    public class AccountController : Controller
    {
        #region Repository
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IRolRepository _rolRepository;

        public AccountController(IKullaniciRepository kullaniciRepository, IRolRepository rolRepository)
        {
            _kullaniciRepository = kullaniciRepository;
            _rolRepository = rolRepository;
        }
        // GET: Account
        #endregion

        #region Account Login
        public ActionResult Login()
        {
            if (Session["KullaniciID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        //[HttpPost]   Eski Yöntem
        //public ActionResult Login(Kullanici k)
        //{
        //    var girisYapanKullanici = _kullaniciRepository.Get(x => x.Email == k.Email && x.Sifre == k.Sifre && x.AktifMi == true);
        //    if (girisYapanKullanici != null)
        //    {
        //        if (girisYapanKullanici.Rol.RolAdi == "Admin")
        //        {
        //            Session["KullaniciID"] = girisYapanKullanici.ID;
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ViewBag.Mesaj = "Yetkisiz kullanıcı";
        //            return View();
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Mesaj = "Kullanıcı bulunamadı";
        //        return View();
        //    }
        //}
        [HttpPost]
        public JsonResult Login(Kullanici k)
        {
            try
            {
                var girisYapanKullanici = _kullaniciRepository.Get(x => x.Email == k.Email && x.Sifre == k.Sifre && x.AktifMi == true);
                if (girisYapanKullanici != null)
                {
                    if (girisYapanKullanici.Rol.RolAdi == "Admin")
                    {
                        Session["KullaniciID"] = girisYapanKullanici.ID;
                        return Json(new ResultJson { Success = true, Message = "Giriş başarılı" });
                    }
                    else
                    {
                        ViewBag.Mesaj = "Yetkisiz kullanıcı";
                        return Json(new ResultJson { Success = false, Message = "Yetkisiz kullanıcı" });
                    }
                }
                else
                {
                    ViewBag.Mesaj = "Kullanıcı bulunamadı";
                    return Json(new ResultJson { Success = false, Message = "Kullanıcı bulunamadı" });
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultJson { Success = false, Message = "Hatalı işlem" });
            }
        }
        #endregion

        #region Index
        [LoginFilter]
        public ActionResult Index(int sayfa = 1)
        {
            return View(_kullaniciRepository.GetAll().OrderByDescending(x => x.EklenmeTarihi).ToPagedList(sayfa, 3));
        }
        #endregion

        #region Kullanıcı Ekle
        [LoginFilter]
        public ActionResult Ekle()
        {
            RolListele();
            return View();
        }
        [HttpPost]
        public JsonResult Ekle(Kullanici k)
        {
            RolListele();
            try
            {
                if (ModelState.IsValid)
                {
                    _kullaniciRepository.Insert(k);
                    _kullaniciRepository.Save();
                    return Json(new ResultJson { Success = true, Message = "Kullanıcı başarıyla eklendi" });
                }
                else
                {
                    return Json(new ResultJson { Success = false, Message = "Eksik kısımları doldurunuz" });
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultJson { Success = false, Message = "Hatalı işlem" });
            }
        }
        #endregion

        #region Kullanıcı Sil
        [LoginFilter]
        public JsonResult Sil(int id)
        {
            Kullanici k = _kullaniciRepository.GetByID(id);
            if (k == null)
            {
                return Json(new ResultJson { Success = false, Message = "Kullanıcı bulunamadı" });
            }
            try
            {
                _kullaniciRepository.Delete(id);
                _kullaniciRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Kullanıcı başarıyla silindi" });
            }
            catch (Exception)
            {
                return Json(new ResultJson { Success = false, Message = "Bir hata oluştu" });
            }
        }
        #endregion

        #region Kullanıcı Düzenle
        [LoginFilter]
        public ActionResult Duzenle(int id)
        {
            RolListele();
            var kullanici = _kullaniciRepository.GetByID(id);
            return View(kullanici);
        }
        [HttpPost]
        public JsonResult Duzenle(Kullanici kullanici)
        {
            RolListele();
            try
            {
                if (ModelState.IsValid)
                {
                    Kullanici k = _kullaniciRepository.GetByID(kullanici.ID);
                    k.RolID = kullanici.RolID;
                    k.AdSoyad = kullanici.AdSoyad;
                    k.Email = kullanici.Email;
                    k.Sifre = kullanici.Sifre;
                    k.AktifMi = kullanici.AktifMi;
                    _kullaniciRepository.Save();
                    return Json(new ResultJson { Success = true, Message = "Düzenleme işlemi başarılı" });
                }
                else
                {
                    return Json(new ResultJson { Success = false, Message = "Eksik kısımları doldurunuz" });
                }

            }
            catch (Exception ex)
            {
                return Json(new ResultJson { Success = false, Message = "Hatalı işlem" });
            }
        }
        #endregion

        #region Metod RolListele
        public void RolListele()
        {
            ViewBag.Roller = _rolRepository.GetAll().ToList();
        }
        #endregion
    }
}
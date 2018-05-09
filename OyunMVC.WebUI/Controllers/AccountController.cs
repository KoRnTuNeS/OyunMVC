using OyunMVC.Core.Infrastructure;
using OyunMVC.Data.Model;
using OyunMVC.WebUI.Class;
using OyunMVC.WebUI.CustomFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OyunMVC.WebUI.Controllers
{
    public class AccountController : Controller
    {
        #region Repository
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IKategoriRepository _kategoriRepository;
        public AccountController(IKullaniciRepository kullaniciRepository, IKategoriRepository kategoriRepository)
        {
            _kullaniciRepository = kullaniciRepository;
            _kategoriRepository = kategoriRepository;
        }
        // GET: Account
        #endregion

        #region Account Login
        public ActionResult Login()
        {
            if (Session["mKullaniciID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Session["Adres"] == null)
            {
                Session["Adres"] = Request.UrlReferrer.ToString();
            }
            return View();
        }
        [HttpPost]
        public JsonResult Login(Kullanici k)
        {
            try
            {
                var girisYapanKullanici = _kullaniciRepository.Get(x => x.Email == k.Email && x.Sifre == k.Sifre && x.AktifMi == true);
                if (girisYapanKullanici != null)
                {
                    Session["mKullaniciID"] = girisYapanKullanici.ID;
                    return Json(new ResultJson { Success = true, Message = "Giriş başarılı" });
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

        #region Account Logout
        [LoginFilter]
        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect(Request.UrlReferrer.ToString());
        }
        #endregion

        #region Account Register
        public ActionResult Register()
        {
            ViewBag.Kategoriler = _kategoriRepository.GetAll();
            if (Session["mKullaniciID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public JsonResult Register(Kullanici k, string SifreTekrar)
        {
            ViewBag.Kategoriler = _kategoriRepository.GetAll();
            try
            {
                if (ModelState.IsValid)
                {
                    if (k.Sifre != SifreTekrar)
                    {
                        return Json(new ResultJson { Success = false, Message = "Şifreler birbiriyle uyuşmuyor" });
                    }
                    k.RolID = 2;
                    _kullaniciRepository.Insert(k);
                    _kullaniciRepository.Save();
                    return Json(new ResultJson { Success = true, Message = "Kayıt işlemi başarılı" });
                }
                else
                {
                    return Json(new ResultJson { Success = false, Message = "Eksik kısımları doldurunuz" });
                }
            }
            catch (Exception)
            {
                return Json(new ResultJson { Success = false, Message = "Hatalı işlem" });
            }
        }
        #endregion
    }
}
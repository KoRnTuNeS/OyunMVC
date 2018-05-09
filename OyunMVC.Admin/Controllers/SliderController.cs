using OyunMVC.Core.Infrastructure;
using OyunMVC.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;
using OyunMVC.Admin.Class;
using OyunMVC.Admin.CustomFilter;

namespace OyunMVC.Admin.Controllers
{
    public class SliderController : Controller
    {
        #region Repository
        private readonly ISliderRepository _sliderRepository;
        private readonly IHaberRepository _haberRepository;
        public SliderController(ISliderRepository sliderRepository, IHaberRepository haberRepository)
        {
            _sliderRepository = sliderRepository;
            _haberRepository = haberRepository;
        }

        // GET: Slider
        #endregion

        #region Index
        [LoginFilter]
        public ActionResult Index(int sayfa = 1)
        {
            var sliderListesi = _sliderRepository.GetAll();
            return View(sliderListesi.OrderByDescending(x => x.ID).ToPagedList(sayfa, 3));
        }
        #endregion

        #region Slider Ekle
        [LoginFilter]
        [ValidateInput(false)]
        public ActionResult Ekle()
        {
            HaberListele();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Ekle(Slider slider, IEnumerable<HttpPostedFileBase> ResimYolu)
        {
            HaberListele();
            if (ModelState.IsValid)
            {
                //if (ResimYolu != null) tekli yöntem
                //{
                //    string dosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                //    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                //    string tamYol = "/External/Slider/" + dosyaAdi + uzanti;
                //    Request.Files[0].SaveAs(Server.MapPath(tamYol));
                //    slider.ResimYolu = tamYol;
                //}
                //_sliderRepository.Insert(slider);
                //_sliderRepository.Save();
                //TempData["Bilgi"] = "Slider başarı ile eklendi";
                //return RedirectToAction("Index", "Slider");
                string cokluResim = System.IO.Path.GetExtension(Request.Files[0].FileName);
                if (cokluResim != "")
                {
                    foreach (var item in ResimYolu)
                    {
                        if (item.ContentLength > 0)
                        {
                            string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                            string Uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                            string TamYol = "/External/Slider/" + DosyaAdi + Uzanti;
                            item.SaveAs(Server.MapPath(TamYol));

                            var resim = new Slider
                            {
                                ResimYolu = TamYol,
                            };
                            resim.HaberID = slider.HaberID;
                            resim.Aciklama = slider.Aciklama.Replace("&ccedil;", "ç").Replace("&yacute;", "ı").Replace("&Ccedil;", "Ç").Replace("&Ouml;", "Ö").Replace("&Uuml;", "Ü").Replace("&ETH;", "Ğ").Replace("&THORN;", "Ş").Replace("&Yacute;", "İ").Replace("&ouml;", "ö").Replace("&thorn;", "ş").Replace("&eth;", "ğ").Replace("&uuml;", "ü").Replace("&yacute;", "ı").Replace("&amp;", "&");
                            resim.AktifMi = slider.AktifMi;
                            resim.Baslik = slider.Baslik;
                            resim.EklenmeTarihi = slider.EklenmeTarihi;
                            _sliderRepository.Insert(resim);
                            _sliderRepository.Save();
                        }
                    }
                }
                TempData["Bilgi"] = "Slider başarı ile eklendi";
                return RedirectToAction("Index", "Slider");
            }
            else
            {
                TempData["Bilgi"] = "Eksik kısımları doldurunuz";
                return View();
            }
        }
        #endregion

        #region Slider Sil
        [LoginFilter]
        public JsonResult Sil(int id)
        {
            Slider s = _sliderRepository.GetByID(id);
            if (s == null)
            {
                throw new Exception("Slider bulunamadı");
            }
            string filename = s.ResimYolu;
            string path = Server.MapPath(filename);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            try
            {
                _sliderRepository.Delete(s.ID);
                _sliderRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Slider başarıyla silindi" });
            }
            catch (Exception ex)
            {
                return Json(new ResultJson { Success = false, Message = "Bir hata oluştu" });
            }
        }
        public ActionResult Duzenle(int id)
        {
            Slider s = _sliderRepository.GetByID(id);
            if (s == null)
            {
                throw new Exception("Slider bulunamadı");
            }
            else
            {
                HaberListele();
                return View(s);
            }
        }
        #endregion

        #region Slider Düzenle
        [LoginFilter]
        [HttpPost]
        public ActionResult Duzenle(Slider slider, HttpPostedFileBase ResimYolu)
        {
            HaberListele();
            if (ModelState.IsValid)
            {
                Slider s = _sliderRepository.GetByID(slider.ID);
                s.Aciklama = slider.Aciklama.Replace("&ccedil;", "ç").Replace("&yacute;", "ı").Replace("&Ccedil;", "Ç").Replace("&Ouml;", "Ö").Replace("&Uuml;", "Ü").Replace("&ETH;", "Ğ").Replace("&THORN;", "Ş").Replace("&Yacute;", "İ").Replace("&ouml;", "ö").Replace("&thorn;", "ş").Replace("&eth;", "ğ").Replace("&uuml;", "ü").Replace("&yacute;", "ı").Replace("&amp;", "&");
                s.AktifMi = slider.AktifMi;
                s.Baslik = slider.Baslik;
                s.HaberID = slider.HaberID;

                if (ResimYolu != null)
                {
                    string filename = s.ResimYolu;
                    string path = Server.MapPath(filename);
                    FileInfo file = new FileInfo(path);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    string yeniDosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                    string yeniUzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string yeniTamYol = "/External/Slider/" + yeniDosyaAdi + yeniUzanti;
                    Request.Files[0].SaveAs(Server.MapPath(yeniTamYol));
                    s.ResimYolu = yeniTamYol;
                }
                _sliderRepository.Save();

                TempData["Bilgi"] = "Güncelleme işlemi başarılı";
                return RedirectToAction("Index", "Slider");
            }
            else
            {
                TempData["Bilgi"] = "Eksik giriş yaptınız";
                return RedirectToAction("Duzenle", "Slider", new { ID = slider.ID });
            }
        }
        #endregion

        #region Metod HaberListele
        public void HaberListele()
        {
            var haberler = _haberRepository.GetAll().ToList();
            ViewBag.Haberler = haberler;
        }
        #endregion
    }
}
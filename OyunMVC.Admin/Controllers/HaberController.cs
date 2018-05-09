using OyunMVC.Admin.Class;
using OyunMVC.Admin.CustomFilter;
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
using System.Text;

namespace OyunMVC.Admin.Controllers
{
    public class HaberController : Controller
    {
        #region Repository
        private readonly IHaberRepository _haberRepository;
        private readonly IKategoriRepository _kategoriRepository;
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IEtiketRepository _etiketRepository;
        private readonly IResimRepository _resimRepository;
        public HaberController(IHaberRepository haberRepository, IKategoriRepository kategoriRepository, IKullaniciRepository kullaniciRepository, IEtiketRepository etiketRepository, IResimRepository resimRepository)
        {
            _haberRepository = haberRepository;
            _kategoriRepository = kategoriRepository;
            _kullaniciRepository = kullaniciRepository;
            _etiketRepository = etiketRepository;
            _resimRepository = resimRepository;
        }
        // GET: Haber
        #endregion

        #region Index
        [LoginFilter]
        public ActionResult Index(int sayfa = 1)
        {
            var haberListesi = _haberRepository.GetAll();
            return View(haberListesi.OrderByDescending(x => x.ID).ToPagedList(sayfa, 3));
        }
        #endregion

        #region Haber Ekle
        [LoginFilter]
        [ValidateInput(false)]
        public ActionResult Ekle()
        {
            KategoriListele();
            return View();
        }
        //[HttpPost] ajax ile resim upload etmesi sıkıntı
        //public JsonResult Ekle(Haber haber, HttpPostedFileBase VitrinResmi, IEnumerable<HttpPostedFileBase> DetayResim, string GelenEtiket)
        //{
        //    KategoriListele();
        //    int SessionControl = (int)HttpContext.Session["KullaniciID"];
        //    Kullanici kullanici = _kullaniciRepository.GetByID(SessionControl);

        //    haber.KullaniciID = kullanici.ID;

        //    if (VitrinResmi != null)
        //    {
        //        string dosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
        //        string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
        //        string tamyol = "/External/Haber/" + dosyaAdi + uzanti;
        //        Request.Files[0].SaveAs(Server.MapPath(tamyol));
        //        haber.Resim = tamyol;
        //    }
        //    try
        //    {
        //        _haberRepository.Insert(haber);
        //        _haberRepository.Save();
        //        return Json(new ResultJson { Success = true, Message = "Haber başarı ile eklendi" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new ResultJson { Success = false, Message = "Hatalı işlem" });
        //    }

        //}
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Ekle(Haber haber, HttpPostedFileBase VitrinResmi, IEnumerable<HttpPostedFileBase> DetayResim, string GelenEtiket)
        {
            KategoriListele();
            int SessionControl = (int)HttpContext.Session["KullaniciID"];
            Kullanici kullanici = _kullaniciRepository.GetByID(SessionControl);

            //if (ModelState.IsValid) ckeditör yüzünden validate kısmını geçemiyo.
            //{
            haber.KullaniciID = kullanici.ID;
            haber.KisaAciklama = haber.KisaAciklama.Replace("&ccedil;", "ç").Replace("&yacute;", "ı").Replace("&Ccedil;", "Ç").Replace("&Ouml;", "Ö").Replace("&Uuml;", "Ü").Replace("&ETH;", "Ğ").Replace("&THORN;", "Ş").Replace("&Yacute;", "İ").Replace("&ouml;", "ö").Replace("&thorn;", "ş").Replace("&eth;", "ğ").Replace("&uuml;", "ü").Replace("&yacute;", "ı").Replace("&amp;", "&");
            haber.Aciklama = haber.Aciklama.Replace("&ccedil;", "ç").Replace("&yacute;", "ı").Replace("&Ccedil;", "Ç").Replace("&Ouml;", "Ö").Replace("&Uuml;", "Ü").Replace("&ETH;", "Ğ").Replace("&THORN;", "Ş").Replace("&Yacute;", "İ").Replace("&ouml;", "ö").Replace("&thorn;", "ş").Replace("&eth;", "ğ").Replace("&uuml;", "ü").Replace("&yacute;", "ı").Replace("&amp;", "&");
            if (VitrinResmi != null)
            {
                string dosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string tamyol = "/External/Haber/" + dosyaAdi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(tamyol));
                haber.Resim = tamyol;
            }
            _haberRepository.Insert(haber);
            _haberRepository.Save();

            _etiketRepository.EtiketEkle(haber.ID, GelenEtiket);

            string cokluResim = System.IO.Path.GetExtension(Request.Files[1].FileName);
            if (cokluResim != "")
            {
                foreach (var item in DetayResim)
                {
                    if (item.ContentLength > 0)
                    {
                        string dosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                        string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                        string tamyol = "/External/Haber/" + dosyaAdi + uzanti;
                        item.SaveAs(Server.MapPath(tamyol));

                        var resim = new Resim
                        {
                            ResimYolu = tamyol
                        };
                        resim.HaberID = haber.ID;
                        _resimRepository.Insert(resim);
                        _resimRepository.Save();
                    }
                }
            }
            TempData["Bilgi"] = "Haber ekleme işlemi başarılı";
            return RedirectToAction("Index", "Haber");
            //}
            //else
            //{
            //    TempData["Bilgi"] = "Eksik kısımları doldurunuz";
            //    return View();
            //}

        }
        #endregion

        #region Haber Sil
        [LoginFilter]
        public JsonResult Sil(int id)
        {
            Haber h = _haberRepository.GetByID(id);
            if (h == null)
            {
                throw new Exception("Haber bulunamadı");
            }

            string filename = h.Resim;
            string path = Server.MapPath(filename);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }

            var detayresimler = _resimRepository.GetMany(x => x.HaberID == id);
            if (detayresimler != null)
            {
                foreach (var item in detayresimler)
                {
                    string drfilename = item.ResimYolu;
                    string drpath = Server.MapPath(drfilename);
                    FileInfo drfile = new FileInfo(drpath);
                    if (drfile.Exists)
                    {
                        drfile.Delete();
                    }
                }
            }
            try
            {
                _haberRepository.Delete(h.ID);
                _haberRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Haber başarı ile silindi" });
            }
            catch (Exception ex)
            {
                return Json(new ResultJson { Success = false, Message = "Haber silerken bir hata oluştu" });
            }
        }
        #endregion

        #region Haber Onay
        [LoginFilter]
        public JsonResult Onay(int id)
        {
            Haber h = _haberRepository.GetByID(id);
            try
            {
                if (h.AktifMi == true)
                {
                    h.AktifMi = false;
                    _haberRepository.Save();
                    return Json(new ResultJson { Success = true, Message = "İşleminiz başarılı" });
                }
                else
                {
                    h.AktifMi = true;
                    _haberRepository.Save();
                    return Json(new ResultJson { Success = true, Message = "İşleminiz başarılı" });
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultJson { Success = false, Message = "Hatalı işlem" });
            }
        }
        #endregion

        #region Haber Düzenle
        [LoginFilter]
        [ValidateInput(false)]
        public ActionResult Duzenle(int id)
        {
            Haber haber = _haberRepository.GetByID(id);
            var haberEtiketler = haber.Etiketler.Select(x => x.EtiketAdi).ToArray();

            HaberEtiketModel model = new HaberEtiketModel
            {
                Haber = haber,
                Etiketler = _etiketRepository.GetAll(),
                GelenEtiketler = haberEtiketler
            };
            StringBuilder birlestir = new StringBuilder();
            foreach (var item in model.GelenEtiketler)
            {
                birlestir.Append(item.ToString());
                birlestir.Append(",");
            }
            model.EtiketAd = birlestir.ToString();
            if (haber == null)
            {
                throw new Exception("Haber bulunamadı");
            }
            else
            {
                KategoriListele();
                return View(model);
            }

        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Duzenle(Haber haber, HttpPostedFileBase VitrinResmi, IEnumerable<HttpPostedFileBase> DetayResim, string EtiketAd)
        {
            KategoriListele();
            if (ModelState.IsValid)
            {
                Haber h = _haberRepository.GetByID(haber.ID);
                h.Aciklama = haber.Aciklama.Replace("&ccedil;", "ç").Replace("&yacute;", "ı").Replace("&Ccedil;", "Ç").Replace("&Ouml;", "Ö").Replace("&Uuml;", "Ü").Replace("&ETH;", "Ğ").Replace("&THORN;", "Ş").Replace("&Yacute;", "İ").Replace("&ouml;", "ö").Replace("&thorn;", "ş").Replace("&eth;", "ğ").Replace("&uuml;", "ü").Replace("&yacute;", "ı").Replace("&amp;", "&");
                h.AktifMi = haber.AktifMi;
                h.Baslik = haber.Baslik;
                h.KategoriID = haber.KategoriID;
                h.KisaAciklama = haber.KisaAciklama.Replace("&ccedil;", "ç").Replace("&yacute;", "ı").Replace("&Ccedil;", "Ç").Replace("&Ouml;", "Ö").Replace("&Uuml;", "Ü").Replace("&ETH;", "Ğ").Replace("&THORN;", "Ş").Replace("&Yacute;", "İ").Replace("&ouml;", "ö").Replace("&thorn;", "ş").Replace("&eth;", "ğ").Replace("&uuml;", "ü").Replace("&yacute;", "ı").Replace("&amp;", "&");

                if (VitrinResmi != null)
                {
                    string dosyaadi = h.Resim;
                    FileInfo file = new FileInfo(Server.MapPath(dosyaadi));
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    string yeniDosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string TamYol = "/External/Haber/" + yeniDosyaAdi + uzanti;
                    Request.Files[0].SaveAs(Server.MapPath(TamYol));
                    h.Resim = TamYol;
                }

                _haberRepository.Save();

                string cokluResim = System.IO.Path.GetExtension(Request.Files[1].FileName);
                if (cokluResim != "")
                {
                    foreach (var item in DetayResim)
                    {
                        if (item.ContentLength > 0)
                        {
                            string drDosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                            string drUzanti = System.IO.Path.GetExtension(Request.Files[1].FileName);
                            string drTamYol = "/External/Haber/" + drDosyaAdi + drUzanti;
                            item.SaveAs(Server.MapPath(drTamYol));

                            var resim = new Resim
                            {
                                ResimYolu = drTamYol
                            };
                            resim.HaberID = haber.ID;
                            _resimRepository.Insert(resim);
                            _resimRepository.Save();
                        }
                    }
                }
                _etiketRepository.EtiketEkle(haber.ID, EtiketAd);

                TempData["Bilgi"] = "Güncelleme işlemi başarılı";
                return RedirectToAction("Index", "Haber");
            }
            else
            {
                TempData["Bilgi"] = "Eksik giriş yaptınız";
                return RedirectToAction("Duzenle", "Haber", new { ID = haber.ID });
            }
        }
        #endregion

        #region Haber Resim Sil
        [LoginFilter]
        public ActionResult ResimSil(int id)
        {
            Resim r = _resimRepository.GetByID(id);
            if (r == null)
            {
                throw new Exception("Resim bulunamadı");
            }
            string dosyaAdi = r.ResimYolu;
            FileInfo file = new FileInfo(Server.MapPath(dosyaAdi));
            if (file.Exists)
            {
                file.Delete();
            }
            _resimRepository.Delete(r.ID);
            _resimRepository.Save();
            return RedirectToAction("Duzenle", "Haber", new { ID = r.HaberID });
        }
        #endregion

        #region Metod KategoriListele
        public void KategoriListele()
        {
            var kategoriListesi = _kategoriRepository.GetAll().ToList();
            ViewBag.Kategoriler = kategoriListesi;
        }
        #endregion
    }
}
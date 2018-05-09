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
    public class KategoriController : Controller
    {
        #region Repository
        private readonly IKategoriRepository _kategoriRepository;
        public KategoriController(IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }
        // GET: Kategori
        #endregion

        #region Index
        [LoginFilter]
        [HttpGet]
        public ActionResult Index(int sayfa = 1)
        {
            return View(_kategoriRepository.GetAll().OrderByDescending(x => x.ID).ToPagedList(sayfa, 3));
        }
        #endregion

        #region Kategori Ekle
        [LoginFilter]
        public ActionResult Ekle()
        {
            KategoriListele();
            return View();
        }

        [HttpPost]
        public JsonResult Ekle(Kategori k)
        {
            try
            {
                _kategoriRepository.Insert(k);
                _kategoriRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Kategori ekleme başarılı" });
            }
            catch (Exception ex)
            {
                return Json(new ResultJson { Success = false, Message = "Kategori eklerken bir hata oluştu" });
            }
        }
        #endregion

        #region Kategori Sil
        [LoginFilter]
        public JsonResult Sil(int id)
        {
            Kategori k = _kategoriRepository.GetByID(id);
            if (k == null)
            {
                return Json(new ResultJson { Success = false, Message = "Kategori bulunamadı" });
            }
            List<Kategori> altKategori = _kategoriRepository.GetMany(x => x.ParentID == k.ID).ToList();
            if (altKategori.Count > 0)
            {
                return Json(new ResultJson { Success = false, Message = "Bu kategorinin alt kategorisi var, öncelikle alt kategoriyi siliniz" });
            }
            try
            {
                _kategoriRepository.Delete(id);
                _kategoriRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Silme işlemi başarılı" });
            }
            catch (Exception ex)
            {
                return Json(new ResultJson { Success = false, Message = "Bir hata oluştu" });
            }
        }
        #endregion

        #region Kategori Düzenle
        [LoginFilter]
        public ActionResult Duzenle(int id)
        {
            Kategori k = _kategoriRepository.GetByID(id);
            if (k == null)
            {
                throw new Exception("Kategori bulunamadı");
            }
            KategoriListele();
            return View(k);
        }
        [HttpPost]
        public JsonResult Duzenle(Kategori kategori)
        {
            try
            {
                Kategori k = _kategoriRepository.GetByID(kategori.ID);
                k.AktifMi = kategori.AktifMi;
                k.KategoriAdi = kategori.KategoriAdi;
                k.ParentID = kategori.ParentID;
                _kategoriRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Düzenleme başarılı" });
            }
            catch (Exception ex)
            {
                return Json(new ResultJson { Success = false, Message = "Hatalı işlem" });
            }
        }
        #endregion

        #region Metod ParentBul
        public string ParentBul(int parentID)
        {
            string a = "";
            if (parentID == 0)
            {
                a = "Boş";
            }
            else
            {
                Kategori parentKategori = _kategoriRepository.GetByID(parentID);
                if (parentKategori == null)
                {
                    a = "Üst kategorisi silinmiş";
                }
                else
                {
                    a = parentKategori.KategoriAdi;
                }
            }
            return a;
        }
        #endregion

        #region Metod KategoriListele
        public void KategoriListele()
        {
            ViewBag.Kategoriler = _kategoriRepository.GetMany(x => x.ParentID == 0).ToList();

        }
        #endregion
    }
}
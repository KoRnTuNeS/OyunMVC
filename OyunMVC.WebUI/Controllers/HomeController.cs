using OyunMVC.Core.Infrastructure;
using OyunMVC.Data.Model;
using OyunMVC.WebUI.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace OyunMVC.WebUI.Controllers
{
    public class HomeController : Controller
    {
        #region Repository
        private readonly ISliderRepository _sliderRepository;
        private readonly IHaberRepository _haberRepository;
        private readonly IKategoriRepository _kategoriRepository;
        private readonly IYorumRepository _yorumRepository;
        public HomeController(ISliderRepository sliderRepository, IHaberRepository haberRepository, IKategoriRepository kategoriRepository, IYorumRepository yorumRepository)
        {
            _sliderRepository = sliderRepository;
            _haberRepository = haberRepository;
            _kategoriRepository = kategoriRepository;
            _yorumRepository = yorumRepository;
        }
        // GET: Home
        #endregion

        #region Index
        public ActionResult Index()
        {
            var haberListesi = _haberRepository.GetAll().ToList();
            ViewBag.Slider = _sliderRepository.GetAll().OrderByDescending(x => x.EklenmeTarihi).Take(5);
            ViewBag.Kategoriler = _kategoriRepository.GetAll();
            ViewBag.Yorumlar = _yorumRepository.GetAll().ToList();
            return View(haberListesi);
        }
        #endregion

        #region İletişim
        public ActionResult Contact()
        {
            ViewBag.Kategoriler = _kategoriRepository.GetAll();
            return View();
        }
        [HttpPost]
        public JsonResult Contact(Mail m)
        {
            ViewBag.Kategoriler = _kategoriRepository.GetAll();
            try
            {
                if (ModelState.IsValid)
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("alpererez@yedioutlet.com");
                    mail.To.Add("alpererez@hotmail.com");
                    mail.IsBodyHtml = true;
                    mail.Subject = m.Konu;
                    mail.Body = string.Format("Gönderen: {0} <br> Adı: {1} <br><br> İçerik: <br> {2}", m.MailAdresi, m.Ad, m.Mesaj);

                    SmtpClient sunucu = new SmtpClient();
                    sunucu.Host = "smtp.isimtescil.net";
                    sunucu.EnableSsl = false;
                    sunucu.Port = 587;
                    sunucu.Credentials = new NetworkCredential("alpererez@yedioutlet.com", "Sd9e9wsS");
                    sunucu.Send(mail);
                    return Json(new ResultJson { Success = true, Message = "Mesajınız tarafımıza yollanmıştır. En kısa sürede geri dönüş yapılacaktır" });
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
    }
}
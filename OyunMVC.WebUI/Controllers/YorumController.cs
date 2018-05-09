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
    public class YorumController : Controller
    {
        #region Repository
        private readonly IYorumRepository _yorumRepository;
        public YorumController(IYorumRepository yorumRepository)
        {
            _yorumRepository = yorumRepository;
        }
        // GET: Yorum
        #endregion

        #region Yorum Ekle
        [LoginFilter]
        [HttpPost]
        public JsonResult Ekle(Yorum y)
        {
            try
            {
                _yorumRepository.Insert(y);
                _yorumRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Yorumunuz eklendi" });
            }
            catch (Exception ex)
            {
                return Json(new ResultJson { Success = false, Message = "Bir hata oluştu" });
            }
        }
        #endregion
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using koala.application.common;
using log4net;
using match.application.common;
using mq.application.common;
using mq.application.service.Interface;
using mq.model.dbentity;
using mq.model.viewentity;

namespace mq.ui.EmployeeWebSite.Controllers
{
    public class HomeController : Controller
    {
        ILog logger = LogManager.GetLogger(typeof(HomeApiController));
        private readonly IBgUserService _bgUserService;
        private readonly IBgLoginLogService _bgLoginLogService;

        public HomeController(IBgUserService bgUserService, IBgLoginLogService bgLoginLogService)
        {
            _bgUserService = bgUserService;
            _bgLoginLogService = bgLoginLogService;
        }

        // GET: Home
        public ActionResult Login()
        {
            ViewBag.SourceUrl = "http://www.baidu.com";
            return View("~/Views/Home/Login.cshtml");
        }

        public JsonResult LoginValidate()
        {
            string sourceUrl = CommonHelper.GetPostValue("url");
            if (!string.IsNullOrEmpty(sourceUrl))
                ViewBag.SourceUrl = sourceUrl;
            sourceUrl = Server.UrlDecode(sourceUrl);
            string username = CommonHelper.GetPostValue("username");
            string password = CommonHelper.GetPostValue("password");
            //string mRemember = CommonHelper.GetPostValue("remember");

            LoginValidateEntity entity = new LoginValidateEntity();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                entity.ErrorCode = "E0011";
                entity.ErrorMessage = ErrorInfoHelper.GetErrorValue(entity.ErrorCode);
                return Json(entity, JsonRequestBehavior.AllowGet);
            }

            string ip = IPHelper.GetLoginIp(System.Web.HttpContext.Current.Request);
            var rSaCryptoService = new RSACryptoService(ProductConfigHelper.ThinkTankJsEncryptPrivateKeyForCSharp, ProductConfigHelper.ThinkTankJsEncryptPublickKeyForCSharp);
            string strDecryptUsername = rSaCryptoService.Decrypt(username);
            string strDecryptPassword = rSaCryptoService.Decrypt(password);
            try
            {
                T_BG_User user = _bgUserService.GetUser(strDecryptUsername, strDecryptPassword);
                if (user != null)
                {
                    if (user.Status == 0)
                    {
                        entity.ErrorCode = "E0003";
                    }
                    else if (user.Status == 1)
                    {
                        string address = LoginHelper.GetMaxMindOmniData(ip);
                        T_BG_LoginLog loginLog = new T_BG_LoginLog { LogUserID = user.ID, IP = ip, Address = address, LogTime = DateTime.Now,IsLogIn = true};
                        long result = _bgLoginLogService.Add(loginLog);
                        entity.ErrorCode = result > 0 ? "E0001" : "E0007";
                        LoginHelper.SetBgUserCookie(user);

                        entity.ToUrl = string.IsNullOrEmpty(sourceUrl) ? "" : sourceUrl;
                    }
                    else if (user.Status == 2)
                    {
                        entity.ErrorCode = "E0004";
                    }
                    else if (user.Status == 3)
                    {
                        entity.ErrorCode = "E0005";
                    }
                    else
                    {
                        entity.ErrorCode = "E0006";
                    }
                    entity.ErrorMessage = ErrorInfoHelper.GetErrorValue(entity.ErrorCode);
                }
            }
            catch (Exception)
            {
                entity.ErrorCode = "E0008";
                entity.ErrorMessage = ErrorInfoHelper.GetErrorValue(entity.ErrorCode);
            }

            return Json(entity, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            LoginHelper.DelBgUserCookie();
            string ip = IPHelper.GetLoginIp(System.Web.HttpContext.Current.Request);
            string address = LoginHelper.GetMaxMindOmniData(ip);
            T_BG_LoginLog loginLog = new T_BG_LoginLog { LogUserID = LoginHelper.UserId.ToInt(0), IP = ip, Address = address, LogTime = DateTime.Now, IsLogIn = true };
            long result = _bgLoginLogService.Add(loginLog);
            return RedirectToAction("Login");
        }

        public ActionResult Error()
        {
            string errorCode = CommonHelper.GetPostValue("ErrorCode");
            string errorMessage = CommonHelper.GetPostValue("ErrorMsg");
            ViewBag.errorCode = errorCode;
            ViewBag.errorMessage = errorMessage;
            return View("~/Views/Share/Error.cshtml");
        }

    }
}
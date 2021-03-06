﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using koala.application.common;
using mq.application.common;
using mq.application.service;

namespace mq.ui.EmployeeWebSite.Controllers
{
    public class MenuController : Controller
    {
        private readonly IBgMenuService _bgMenuService;
        public MenuController(IBgMenuService bgMenuService)
        {
            _bgMenuService = bgMenuService;
        }

        // GET: Menu
        public ActionResult LeftMenu()
        {
            int channelId = CommonHelper.GetPostValue("cid").ToInt(0);
            ViewBag.ChannelId = channelId;
            long userId = LoginHelper.UserId;
            var menuList = _bgMenuService.GetBgMenuByUserId(1);
            return PartialView(menuList);
        }

        public ActionResult LeftMenu1()
        {
            int channelId = CommonHelper.GetPostValue("cid").ToInt(0);
            ViewBag.ChannelId = channelId;
            long userId = LoginHelper.UserId;
            var menuList = _bgMenuService.GetBgMenuByUserId(1);
            return PartialView(menuList);
        }

        public ActionResult LeftMenu2()
        {
            int channelId = CommonHelper.GetPostValue("cid").ToInt(0);
            ViewBag.ChannelId = channelId;
            long userId = LoginHelper.UserId;
            var menuList = _bgMenuService.GetBgMenuByUserId(1);
            return PartialView(menuList);
        }

		/// <summary>
		/// 为_MainPage母版页准备的侧面导航栏
		/// </summary>
		/// <returns></returns>
		public ActionResult LeftMainPage()
		{
			int channelId = CommonHelper.GetPostValue("cid").ToInt(0);
			ViewBag.ChannelId = channelId;
			long userId = LoginHelper.UserId;
			var menuList = _bgMenuService.GetBgMenuByUserId(1);
			return PartialView(menuList);
		}

		public ActionResult Error()
        {
            string errorCode = CommonHelper.GetPostValue("ErrorCode");
            string errorMessage = CommonHelper.GetPostValue("ErrorMsg");
			errorMessage = HttpUtility.UrlDecode(errorMessage);

			ViewBag.errorCode = errorCode;
            ViewBag.errorMessage = errorMessage;
            return View("~/Views/Share/Error.cshtml");
        }
    }
}
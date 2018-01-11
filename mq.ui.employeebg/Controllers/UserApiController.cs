using mq.application.common;
using mq.application.service;
using mq.application.service.Interface;
using mq.model.viewentity.employeebg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace mq.ui.employeebg.Controllers
{
    public class UserApiController : ApiController
    {
		private readonly IBgAreaService _areaService;
		private readonly IBgShopService _bgShopService;
		private readonly IBgUserService _bgUserService;
		private readonly IBgUserExtendService _bgUserExtendService;
		

		public UserApiController(IBgAreaService areaService, IBgShopService bgShopService, IBgUserService bgUserService, IBgUserExtendService bgUserExtendService)
		{
			_areaService = areaService;
			_bgShopService = bgShopService;
			_bgUserService = bgUserService;
			_bgUserExtendService = bgUserExtendService;
		}

		[System.Web.Http.HttpPost]
		[System.Web.Http.HttpGet]
		public JsonUserGetShopListEntity GetShopByAreaId()
		{
			long areaId = CommonHelper.GetPostValue("areaId").ToLong(-1);
			JsonUserGetShopListEntity result = new JsonUserGetShopListEntity();
			if (areaId<0)
			{
				result.ErrorCode = "E001";
				result.ErrorMessage = "未获得区域信息！";
				return result;
			}
			result.ShopList = _bgShopService.List(areaId);
			result.ErrorCode = "E000";
			return result;
		}

		[System.Web.Http.HttpPost]
		[System.Web.Http.HttpGet]
		public JsonUserGetListEntity GetUserList()
		{
			long areaId = CommonHelper.GetPostValue("areaId").ToLong(-999);
			long shopId = CommonHelper.GetPostValue("shopId").ToLong(-999);
			string realName = CommonHelper.GetPostValue("realName");
			int status = CommonHelper.GetPostValue("status").ToInt(-1);
			JsonUserGetListEntity result = new JsonUserGetListEntity();
			if (status==-1&&(areaId == -999 || shopId ==-999))
			{
				result.ErrorCode = "E001";
				result.ErrorMessage = "获得信息不全！";
				return result;
			}
			realName = HttpUtility.UrlDecode(realName);
			result.UserExtendList = _bgUserExtendService.GetListByAreaIdShopIdShopName(realName,areaId, shopId, status);
			result.ErrorCode = "E000";
			return result;
		}

	}
}

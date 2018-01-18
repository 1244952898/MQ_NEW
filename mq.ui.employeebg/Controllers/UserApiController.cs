using mq.application.common;
using mq.application.service;
using mq.application.service.Interface;
using mq.model.dbentity;
using mq.model.viewentity;
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

		[System.Web.Http.HttpPost]
		[System.Web.Http.HttpGet]
		public JsonApproveEmployPassEntity ApproveEmployPass()
		{
			JsonApproveEmployPassEntity result = new JsonApproveEmployPassEntity();
			long id = CommonHelper.GetPostValue("id").ToLong(-1);
			int status = CommonHelper.GetPostValue("status").ToInt(-1);
			if (id<0|| status<0)
			{
				result.ErrorCode = "E001";
				result.ErrorMessage = "获得信息不全！";
				return result;
			}

			T_BG_User user = _bgUserService.GetUserById(id);
			if (user==null)
			{
				result.ErrorCode = "E002";
				result.ErrorMessage = "该角色已经被删除！";
				return result;
			}
			if (user.Status!=0)
			{
				result.ErrorCode = "E004";
				result.ErrorMessage = "该角色已经修改，请刷新页面！";
				return result;
			}

			user.Status = status;
			user.ApproveTime = DateTime.Now;
			user.ApproveName = LoginHelper.UserName;
			bool success = _bgUserService.Update(user);
			if (success)
			{
				result.ErrorCode = "E000";
				result.ErrorMessage = "成功";
			}
			else
			{
				result.ErrorCode = "E003";
				result.ErrorMessage = "更新失败！";
			}
			return result;
		}

		[System.Web.Http.HttpPost]
		[System.Web.Http.HttpGet]
		public JsonLeavePositionEntity LeavePosition() {
			JsonLeavePositionEntity result = new JsonLeavePositionEntity();

			long id = CommonHelper.GetPostValue("id").ToLong(-1);
			if (id < 0)
			{
				result.ErrorCode = "E001";
				result.ErrorMessage = "获得信息不全！";
				return result;
			}

			T_BG_User user = _bgUserService.GetUserById(id);
			if (user == null)
			{
				result.ErrorCode = "E002";
				result.ErrorMessage = "该角色不存在，请刷新页面！";
				return result;
			}
			else if (user.IsDel==1)
			{
				result.ErrorCode = "E002";
				result.ErrorMessage = "该角色已经被删除，请刷新页面！";
				return result;
			}
			else if (user.Status == 0)
			{
				result.ErrorCode = "E002";
				result.ErrorMessage = "该员工尚未审核通过，无法离职！";
				return result;
			}
			else if (user.Status == 2)
			{
				result.ErrorCode = "E002";
				result.ErrorMessage = "该员工尚未审核通过，无法离职！";
				return result;
			}
			else if (user.Status == 3)
			{
				result.ErrorCode = "E002";
				result.ErrorMessage = "该员已经离职，请刷新页面！";
				return result;
			}

			user.Status = 3;
			bool success = _bgUserService.Update(user);
			if (success)
			{
				result.ErrorCode = "E000";
				result.ErrorMessage = "成功";
			}
			else
			{
				result.ErrorCode = "E003";
				result.ErrorMessage = "更新失败！";
			}
			return result;
		}
	}
}

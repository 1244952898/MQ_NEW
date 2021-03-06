﻿using log4net;
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
		ILog logger = log4net.LogManager.GetLogger(typeof(DisplayGuideApiController));
		private readonly IBgAreaService _areaService;
		private readonly IBgShopService _bgShopService;
		private readonly IBgUserService _bgUserService;
		private readonly IBgUserExtendService _bgUserExtendService;
		private readonly IBGChangeUserPositionService _bgChangeUserPositionService;
		//private readonly IBgPositionService _bgPositionService;

		public UserApiController(IBgAreaService areaService, IBgShopService bgShopService, IBgUserService bgUserService, IBgUserExtendService bgUserExtendService, IBGChangeUserPositionService bgChangeUserPositionService)
		{
			_areaService = areaService;
			_bgShopService = bgShopService;
			_bgUserService = bgUserService;
			_bgUserExtendService = bgUserExtendService;
			_bgChangeUserPositionService = bgChangeUserPositionService;
			//_bgPositionService = bgPositionService;
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

		/// <summary>
		/// 员工调动
		/// </summary>
		/// <returns></returns>
		[System.Web.Http.HttpPost]
		[System.Web.Http.HttpGet]
		public JsonChangePositionEntity ChangePosition() {
			JsonChangePositionEntity result = new JsonChangePositionEntity();
			long id = CommonHelper.GetPostValue("id").ToLong(-1);
			long newAreaId = CommonHelper.GetPostValue("areaId").ToLong(-1);
			long newShopId = CommonHelper.GetPostValue("shopId").ToLong(-1);
			if (id==-1 || newAreaId == -1 || newShopId == -1)
			{
				result.ErrorCode = "E001";
				result.ErrorMessage = "获得信息不全！";
				return result;
			}

			T_BG_User user = _bgUserService.GetUserById(id);
			if (user == null)
			{
				result.ErrorCode = "E002";
				result.ErrorMessage = "未发现该用户！";
				return result;
			}

			if (user.Lvl>=LoginHelper.Lvl)
			{
				result.ErrorCode = "E002";
				result.ErrorMessage = "您无权调动该员工职位！";
				return result;
			}

			T_BG_ChangeUserPosition c = _bgChangeUserPositionService.GetByUserId(id);
			if (c!=null&& c.Status==0)
			{
				result.ErrorCode = "E003";
				result.ErrorMessage = "该員工已经调整岗位，正在审核中！";
				return result;
			}

			if (user.AreaId==newAreaId&&user.ShopID==newShopId)
			{
				result.ErrorCode = "E004";
				result.ErrorMessage = "该員工已经已经是这个店铺的员工，无需调动！";
				return result;
			}

			T_BG_ChangeUserPosition changeUserPosition = new T_BG_ChangeUserPosition();
			changeUserPosition.UserId = id;
			changeUserPosition.OldAreaId = user.AreaId.ToLong(-1);
			changeUserPosition.OldShopId = user.ShopID.ToLong(-1);
			changeUserPosition.NewAreaId = newAreaId;
			changeUserPosition.NewShopId = newShopId;
			changeUserPosition.AddTime = DateTime.Now;
			changeUserPosition.IsDel = 0;
			changeUserPosition.PassUserId = -1;
			changeUserPosition.PassUserName = "";
			changeUserPosition.Status = 0;

			bool success = _bgChangeUserPositionService.Add(changeUserPosition);
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

		/// <summary>
		/// 员工调动确认
		/// </summary>
		/// <returns></returns>
		[System.Web.Http.HttpPost]
		[System.Web.Http.HttpGet]
		public JsonEnsureChangePositionEntity EnsureChangePosition() {
			JsonEnsureChangePositionEntity result = new JsonEnsureChangePositionEntity();
			long id = CommonHelper.GetPostValue("id").ToLong(-1);
			int status = CommonHelper.GetPostValue("status").ToInt(-1);
			if (id == -1|| status==-1)
			{
				result.ErrorCode = "E001";
				result.ErrorMessage = "获得信息不全！";
				return result;
			}
			T_BG_ChangeUserPosition changeUserPosition = _bgChangeUserPositionService.GetByChangeUserPositionId(id);
			if (changeUserPosition==null)
			{
				result.ErrorCode = "E002";
				result.ErrorMessage = "未发现调动信息！";
				return result;
			}
			if (changeUserPosition.Status==1)
			{
				result.ErrorCode = "E002";
				result.ErrorMessage = "用户已经完成调动！";
				return result;
			}

			changeUserPosition.PassUserId = LoginHelper.UserId;
			changeUserPosition.PassUserName = LoginHelper.UserName;
			changeUserPosition.PassDateTime = DateTime.Now;
			changeUserPosition.Status = status;
			if (_bgChangeUserPositionService.Update(changeUserPosition))
			{
				T_BG_User user = _bgUserService.GetUserById(changeUserPosition.UserId);
				user.ShopID = changeUserPosition.NewShopId.ToInt(-1);
				user.AreaId = changeUserPosition.NewAreaId;
				bool suc = _bgUserService.Update(user);
				if (suc)
				{
					result.ErrorCode = "E000";
					result.ErrorMessage = "成功！";
					return result;
				}
				else
				{
					logger.ErrorFormat("调动员工更新员工新位置信息失败，请处理员工信息：userid={0};user.ShopID={1};user.AreaId={2};登录用户id={3}", user.ID, user.ShopID, user.AreaId, LoginHelper.UserId);
					result.ErrorCode = "E003";
					result.ErrorMessage = "更新员工数据失败！";
				}
			}
			else
			{
				result.ErrorCode = "E003";
				result.ErrorMessage = "更新调动数据失败！";
			}	
			return result;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using koala.application.common;
using mq.application.common;
using mq.application.service;
using mq.application.service.Interface;
using mq.model.dbentity;
using mq.model.viewentity;
using mq.model.viewentity.employeebg;
using mq.model.extendedentity;
using mq.application.webmvc;

namespace mq.ui.employeebg.Controllers
{
    public class UserController : Controller
    {
		
		private readonly IBgRoleService _bgRoleService;
        private readonly IBgAreaService _areaService;
        private readonly IBgShopService _bgShopService;
        private readonly IBgDepartmentService _bgDepartmentService;
        private readonly IBgUserService _bgUserService;
        private readonly IBgVUserAreaRoleDepartmentService _bgVUser;
        private readonly IBgPositionService _bgPositionService;
		private readonly IBgShortStaticFieldService _bgShortStaticFieldService;
		private readonly IBgUserExtendService _bgUserExtendService;

		public UserController(IBgRoleService bgRoleService, IBgAreaService areaService, IBgShopService bgShopService, IBgDepartmentService bgDepartmentService, IBgUserService bgUserService, IBgVUserAreaRoleDepartmentService bgVUser, IBgPositionService bgPositionService,IBgShortStaticFieldService bgShortStaticFieldService, IBgUserExtendService bgUserExtendService)
        {
            _bgRoleService = bgRoleService;
            _areaService = areaService;
            _bgShopService = bgShopService;
            _bgDepartmentService = bgDepartmentService;
            _bgUserService = bgUserService;
            _bgVUser = bgVUser;
			_bgPositionService = bgPositionService;
			_bgShortStaticFieldService = bgShortStaticFieldService;
			_bgUserExtendService = bgUserExtendService;
		}

        // GET: User
        public ActionResult Add()
        {
			int id = CommonHelper.GetPostValue("id").ToInt(-1);
            UserAddEntity entity = new UserAddEntity();
            entity.RoleList = _bgRoleService.List();
			long positionId = LoginHelper.PositionId;
			long departmentId = LoginHelper.DepartmentId;
			entity.PositionList = _bgPositionService.GetlistByLvlAndDepartmentId(positionId, departmentId);
			entity.ShortStaticFieldList = _bgShortStaticFieldService.GetListByType(0);
			entity.CardFieldList = _bgShortStaticFieldService.GetListByType(1);
			if (id > 0)
			{
				entity.BgUser = _bgUserService.GetUserById(id);
				if (entity.BgUser==null)
				{
					string msg = HttpUtility.HtmlEncode("未获得该用户信息");
					string url = DomainUrlHelper.EmployeeBgPath + "/menu/error?ErrorCode=E000&ErrorMsg=" + msg;
					return Redirect(url);
				}
			}
			ViewBag.add = id<0;
			return View(entity);
        }

        public ActionResult List()
        {
            
            return View();
        }

        public ActionResult GetPartialList()
        {
            List<V_User_Area_Role_Department_Shop> list = _bgVUser.GetList();
            return PartialView(list);
        }

		/// <summary>
		/// 添加或者修改用户
		/// </summary>
		/// <returns></returns>
        public JsonResult AddOrUpdateUser()
        {
            JsonUserAddUserEntity json = new JsonUserAddUserEntity();
            string realname = CommonHelper.GetPostValue("realname");
			int gender = CommonHelper.GetPostValue("gender").ToInt(-1);
			long positionId = CommonHelper.GetPostValue("positionId").ToLong(-1);
			DateTime entryDate = CommonHelper.GetPostValue("entryDate").ToDateTime(DateTime.MaxValue);
			string identity = CommonHelper.GetPostValue("identity");
			string phone = CommonHelper.GetPostValue("phone");
			long educationId = CommonHelper.GetPostValue("educationId").ToLong(-1);
			long householdId = CommonHelper.GetPostValue("householdId").ToLong(-1);
			string school = CommonHelper.GetPostValue("school");
			string email = CommonHelper.GetPostValue("email");
			string emergency = CommonHelper.GetPostValue("emergency");
			string address = CommonHelper.GetPostValue("address");
			string remark = CommonHelper.GetPostValue("remark");
			int add= CommonHelper.GetPostValue("add").ToInt(0);//0=添加 1=修改
			long id = CommonHelper.GetPostValue("id").ToLong(-1);

			if ((string.IsNullOrEmpty(realname) || gender == -1 || positionId == -1 || entryDate == DateTime.MaxValue || string.IsNullOrEmpty(identity)|| householdId==-1|| string.IsNullOrEmpty(email) || string.IsNullOrEmpty(emergency) || string.IsNullOrEmpty(address))||(add==1&& id<0))
            {
                json.ErrorCode = "E001";
                json.ErrorMessage = "参数不全！";
                return Json(json);
            }

			realname = HttpUtility.UrlDecode(realname);
			school = HttpUtility.UrlDecode(school);
			emergency = HttpUtility.UrlDecode(emergency);
			address = HttpUtility.UrlDecode(address);
			remark = HttpUtility.UrlDecode(remark);

			T_BG_User user = null;
			if (add==0)
			{
				user = new T_BG_User();
			}
			else
			{
				user = _bgUserService.GetUserById(id);
				if (user == null)
				{
					json.ErrorCode = "E003";
					json.ErrorMessage = "未获得该修改对象！";
					return Json(json);
				}
			}
			
            user.RealName = realname;
            user.Gender = gender;
            user.PositionId = positionId;
            user.EntryDate = entryDate;
            user.IP = identity;
            user.Phone = phone;
            user.EducationId = educationId;
            user.HouseholdId = householdId;
			user.School = school;
			user.Email = email;
			user.Emergency = emergency;
			user.Address = address;
			user.Remark = remark;

			bool result = false;
			if (add==0)
			{
				user.DepartmentId = LoginHelper.DepartmentId;
				user.Status = 0;
				user.AddTime = DateTime.Now;
				user.IsDel = 0;
				user.AreaId = LoginHelper.AreaId;
				user.ShopID=LoginHelper.ShopID;//这个后期可能需要修改
				user.RoleID = 2;
				result = _bgUserService.Add(user);
			}
			else
			{
				result = _bgUserService.Update(user);
			}
         
            if (result)
            {
                json.ErrorCode = "E000";
                json.ErrorMessage = "成功！";
            }
            else
            {
                json.ErrorCode = "E002";
                json.ErrorMessage = "数据库操作失败！";

            }
            return Json(json);
        }
        public ActionResult GetShopList()
        {
            long areaId = CommonHelper.GetPostValue("areaId").ToLong(-1L);
            List<T_BG_Shop> shopList = _bgShopService.List(areaId);
            return PartialView(shopList);
        }

        public JsonResult CheckUserName()
        {
            string username = CommonHelper.GetPostValue("username");
            username = HttpUtility.UrlDecode(username);
            if (string.IsNullOrEmpty(username))
            {
                return Json(new { ErrorCode = "E000", ErrorMsg = "用户名不能为空" });
            }
            bool result = _bgUserService.Check(username);
            if (result)
            {
                return Json(new { ErrorCode = "E001", ErrorMsg = "用户名已存在！" });
            }
            else
            {
                return Json(new { ErrorCode = "E000", ErrorMsg = "用户名不能为空" });
            }
        }

        public JsonResult Pass()
        {
            JsonUserPassEntity json=new JsonUserPassEntity();
            long id = CommonHelper.GetPostValue("id").ToLong(-1L);
            int state = CommonHelper.GetPostValue("state").ToInt(0);
            if (id < 0)
            {
                json.ErrorCode = "E001";
                json.ErrorMessage = "参数不全！";
                return Json(json);
            }

            T_BG_User user = _bgUserService.GetUserById(id);
            if (user==null)
            {
                json.ErrorCode = "E002";
                json.ErrorMessage = "未获得该对象，请刷新页面！";
                return Json(json);
            }
            user.Status = state;
            user.ApproveName = LoginHelper.UserName;
            user.ApproveTime=DateTime.Now;
            bool result = _bgUserService.Update(user);
            if (result)
            {
                json.ErrorCode = "E000";
                json.ErrorMessage = "成功！";
            }
            else
            {
                json.ErrorCode = "E002";
                json.ErrorMessage = "失败，请刷新页面！";
            } 
            return Json(json);
        }

		public ActionResult EmployList() {
			UserEmployListEntity entity = new UserEmployListEntity();
			entity.AreaList = _areaService.List();
			long areaId = LoginHelper.AreaId;
			long shopID = LoginHelper.ShopID;
			if (entity.AreaList != null && entity.AreaList.Count > 0)
			{
				entity.ShopList = _bgShopService.List(areaId);
			}
			ViewBag.areaId = areaId;
			ViewBag.shopID = shopID;
			return View(entity);
		}

		public ActionResult ApproveEmploy()
		{
			//UserApproveEmployEntity entity = new UserApproveEmployEntity();
			long positionId = LoginHelper.PositionId;
			List<BgUserExtend> ApproveList= _bgUserExtendService.GetApproveList(positionId);
			return View(ApproveList);
		}
	}
}
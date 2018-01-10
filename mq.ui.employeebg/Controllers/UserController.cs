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

	  public UserController(IBgRoleService bgRoleService, IBgAreaService areaService, IBgShopService bgShopService, IBgDepartmentService bgDepartmentService, IBgUserService bgUserService, IBgVUserAreaRoleDepartmentService bgVUser, IBgPositionService bgPositionService,IBgShortStaticFieldService bgShortStaticFieldService)
        {
            _bgRoleService = bgRoleService;
            _areaService = areaService;
            _bgShopService = bgShopService;
            _bgDepartmentService = bgDepartmentService;
            _bgUserService = bgUserService;
            _bgVUser = bgVUser;
			_bgPositionService = bgPositionService;
			_bgShortStaticFieldService = bgShortStaticFieldService;
		}

        // GET: User
        public ActionResult Add()
        {
            UserAddEntity entity = new UserAddEntity();
            entity.RoleList = _bgRoleService.List();
			long positionId = LoginHelper.PositionId;
			long departmentId = LoginHelper.DepartmentId;
			entity.PositionList = _bgPositionService.GetlistByLvlAndDepartmentId(positionId, departmentId);
			entity.ShortStaticFieldList = _bgShortStaticFieldService.GetListByType(0);
			entity.CardFieldList = _bgShortStaticFieldService.GetListByType(1);
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


        public JsonResult AddUser()
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
			if (string.IsNullOrEmpty(realname) || gender == -1 || positionId == -1 || entryDate == DateTime.MaxValue || string.IsNullOrEmpty(identity)|| householdId==-1|| string.IsNullOrEmpty(email) || string.IsNullOrEmpty(emergency) || string.IsNullOrEmpty(address))
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
			T_BG_User user = new T_BG_User();
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

			user.DepartmentId = LoginHelper.DepartmentId;
			user.Status = 0;
			user.AddTime = DateTime.Now;
            user.IsDel = 0;
			user.AreaId = LoginHelper.AreaId;
			user.RoleID = 2;

            bool result = _bgUserService.Add(user);

            if (result)
            {
                json.ErrorCode = "E000";
                json.ErrorMessage = "添加成功！";
            }
            else
            {
                json.ErrorCode = "E002";
                json.ErrorMessage = "添加失败！";

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

	}
}
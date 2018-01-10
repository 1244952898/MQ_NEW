﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.model.viewentity.employeebg
{
    public class UserAddEntity
    {
        public List<T_BG_Role> RoleList { get; set; }
        public List<T_BG_Area> AreaList { get; set; }
        public List<T_BG_Shop> ShopList { get; set; }
        public List<T_BG_Department> DepartmentList { get; set; }
        public List<T_BG_Position> PositionList { get; set; }
        public List<T_Bg_ShortStaticField> ShortStaticFieldList { get; set; }
        public List<T_Bg_ShortStaticField> CardFieldList { get; set; }
	}
	public class UserEmployListEntity
	{
		public List<T_BG_Area> AreaList { get; set; }
		public List<T_BG_Shop> ShopList { get; set; }
	}
}

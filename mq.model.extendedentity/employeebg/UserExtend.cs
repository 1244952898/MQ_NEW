using mq.model.dbentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.model.extendedentity
{
	public class BgUserExtend:T_BG_User
	{
		public string RoleName { get; set; }
		public string ShopName { get; set; }
		public string DepartmentName { get; set; }
		public string AreaName { get; set; }
		public string PositionName { get; set; }
		public string EducationName { get; set; }
		public string HouseholdName { get; set; }
	}
}

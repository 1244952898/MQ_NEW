using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.model.extendedentity.employeebg
{
	public class BgChangeUserPositionExtend:T_BG_ChangeUserPosition
	{
		public string OldAreaName { get; set; }
		public string OldShopName { get; set; }
		public string NewAreaName { get; set; }
		public string NewShopName { get; set; }
		public string PositionName { get; set; }
		public string UserName { get; set; }
		public DateTime UserAddTime { get; set; }
	}
}

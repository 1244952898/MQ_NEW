using mq.model.dbentity;
using mq.model.extendedentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.model.viewentity.employeebg
{
	public class JsonUserGetListEntity:JsonBaseEntity
	{
		public List<BgUserExtend> UserExtendList { get; set; }
	}
}

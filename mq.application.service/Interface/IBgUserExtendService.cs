using mq.model.extendedentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.application.service.Interface
{
	public interface IBgUserExtendService
	{
		List<BgUserExtend> GetListByAreaIdShopIdShopName(string shopName, long areaId = -1, long shopId = -1);
	}
}

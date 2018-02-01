using mq.model.extendedentity.employeebg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.application.service.Interface
{
	public interface IBgChangeUserPositionExtendService
	{
		List<BgChangeUserPositionExtend> GetList(long departmentId, long lvl);
	}
}

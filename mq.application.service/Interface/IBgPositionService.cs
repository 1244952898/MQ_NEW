using mq.model.dbentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.application.service
{
	public interface IBgPositionService
	{
		List<T_BG_Position> GetlistByLvlAndDepartmentId(long positionId, long departmentId);
		T_BG_Position GetByPositionId(long positionId);
	}
}

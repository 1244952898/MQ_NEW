using mq.model.dbentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.application.service.Interface
{
	public interface IBGChangeUserPositionService
	{
		bool Add(T_BG_ChangeUserPosition changeUserPosition);
		T_BG_ChangeUserPosition Get(long id);
	}
}

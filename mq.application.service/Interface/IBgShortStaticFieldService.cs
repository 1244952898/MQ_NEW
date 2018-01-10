using mq.model.dbentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.application.service
{
	public interface IBgShortStaticFieldService
	{
		List<T_Bg_ShortStaticField> GetListByType(int type);
	}
}

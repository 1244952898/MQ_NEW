using DapperExtensions;
using mq.application.service.Interface;
using mq.dataaccess.sql.Interface;
using mq.model.dbentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.application.service.Implement
{
	
	public class BGChangeUserPositionService : IBGChangeUserPositionService
	{
		private static IBGChangeUserPositionRepository _iBGChangeUserPositionRepository;
		public BGChangeUserPositionService(IBGChangeUserPositionRepository iBGChangeUserPositionRepository)
		{
			_iBGChangeUserPositionRepository = iBGChangeUserPositionRepository;
		}

		public bool Add(T_BG_ChangeUserPosition changeUserPosition)
		{
			try
			{
				return _iBGChangeUserPositionRepository.Add(changeUserPosition)>0;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public T_BG_ChangeUserPosition Get(long id)
		{
			try
			{
				PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
				pmain.Predicates.Add(Predicates.Field<T_BG_ChangeUserPosition>(f => f.UserId, Operator.Eq, id));
				pmain.Predicates.Add(Predicates.Field<T_BG_ChangeUserPosition>(f => f.IsDel, Operator.Eq, 0));
				return	_iBGChangeUserPositionRepository.GetModel(pmain);
			}
			catch (Exception)
			{
				return null;
			}
			
		}

	}
}

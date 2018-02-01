using mq.dataaccess.sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;
using Dapper;
using DapperExtensions;

namespace mq.application.service.Implement
{
	public class BgPositionService: IBgPositionService
	{
		private readonly string Colnum = @"
											[PositionId]
											,[DepartmentId]
											,[Lvl]
											,[AddTime]
											,[IsDel]
											,[PositionName]
										";
		private IBgPositionRepository _bgPositionRepository;
		public BgPositionService(IBgPositionRepository bgPositionRepository) {
			_bgPositionRepository = bgPositionRepository;
		}



		public List<T_BG_Position> GetlistByLvlAndDepartmentId(long positionId, long departmentId) {
			try
			{
				//PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
				//pmain.Predicates.Add(Predicates.Field<T_BG_Position>(f => f.PositionId, Operator.Eq, positionId));
				//pmain.Predicates.Add(Predicates.Field<T_BG_Position>(f => f.IsDel, Operator.Eq, 0));

				T_BG_Position position = GetByPositionId(positionId);
				if (position==null)
					return null;
				string sql = @"
								SELECT {0}
								FROM [POS].[dbo].[T_BG_Position]
								WHERE DepartmentId=@DepartmentId and Lvl<@Lvl
                                ";
				sql = string.Format(sql, Colnum);
				DynamicParameters dynParams = new DynamicParameters();
				dynParams.Add("@DepartmentId", departmentId);
				dynParams.Add("@Lvl", position.Lvl);
				return _bgPositionRepository.QueryList(sql, dynParams).ToList();
			}
			catch (Exception ex)
			{
			}
			return null;
		}

		public T_BG_Position GetByPositionId(long positionId)
		{
			try
			{
				PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
				pmain.Predicates.Add(Predicates.Field<T_BG_Position>(f => f.PositionId, Operator.Eq, positionId));
				pmain.Predicates.Add(Predicates.Field<T_BG_Position>(f => f.IsDel, Operator.Eq, 0));
				return _bgPositionRepository.GetModel(pmain);
			}
			catch (Exception )
			{
				return null;
			}
		}

	}
}

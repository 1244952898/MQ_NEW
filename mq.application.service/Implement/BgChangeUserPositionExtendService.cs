using Dapper;
using mq.application.service.Interface;
using mq.dataaccess.sql.Interface;
using mq.model.extendedentity.employeebg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.application.service.Implement
{
	public class BgChangeUserPositionExtendService : IBgChangeUserPositionExtendService
	{
		private static IBgChangeUserPositionExtendRepository _iBgChangeUserPositionExtendRepository;
		public BgChangeUserPositionExtendService(IBgChangeUserPositionExtendRepository iBgChangeUserPositionExtendRepository)
		{
			_iBgChangeUserPositionExtendRepository = iBgChangeUserPositionExtendRepository;
		}

		public List<BgChangeUserPositionExtend> GetList(long departmentId,long lvl)
		{
			try
			{
				//数据过大，可能会慢点，变成存储过程
				string sqlString = @"
										SELECT A.*,C.AreaName AS 'OldAreaName'
										,D.Name AS 'OldShopName',E.AreaName AS 'NewAreaName'
										,F.Name AS 'NewShopName',G.PositionName AS 'PositionName'
										,B.Name AS 'UserName' ,B.AddTime AS 'UserAddTime'
										FROM [POS].[dbo].[T_BG_ChangeUserPosition] A
										LEFT JOIN T_BG_User B ON A.UserId=B.ID
										LEFT JOIN T_BG_Area C ON A.OldAreaId=C.ID
										LEFT JOIN T_BG_Shop D ON A.OldShopId=D.ID
										LEFT JOIN T_BG_Area E ON A.NewAreaId=E.ID
										LEFT JOIN T_BG_Shop F ON A.NewShopId=F.ID
										LEFT JOIN T_BG_Position G ON B.PositionId=G.PositionId
										WHERE B.DepartmentId=@DepartmentId 
										AND G.Lvl<@lvl
										ORDER BY A.Status ASC,A.AddTime DESC
                                    ";
				DynamicParameters dynParams = new DynamicParameters();
				dynParams.Add("DepartmentId", departmentId);
				dynParams.Add("lvl", lvl);
				var list = _iBgChangeUserPositionExtendRepository.QueryList(sqlString, dynParams, false);
				return list.ToList();
			}
			catch (Exception)
			{
				return null;
			}
		}

	}
}
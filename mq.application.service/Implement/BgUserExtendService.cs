using Dapper;
using mq.application.service.Interface;
using mq.dataaccess.sql.Interface;
using mq.model.extendedentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.application.service.Implement
{
	public class BgUserExtendService : IBgUserExtendService
	{
		private static IBgUserExtendRepository _bgUserExtendRepository;
		public BgUserExtendService(IBgUserExtendRepository bgUserExtendRepository)
		{
			_bgUserExtendRepository = bgUserExtendRepository;
		}
		public List<BgUserExtend> GetListByAreaIdShopIdShopName(string realName, long areaId = -1, long shopId = -1, int status = -1)
		{
			try
			{
				string sql = @"
								SELECT A.*,B.Name AS ShopName,C.DepartmentName ,D.AreaName,E.PositionName,
								F.ShortName AS EducationName,G.ShortName AS HouseholdName
								FROM T_BG_User AS A
								JOIN T_BG_Shop AS B ON A.ShopID=B.ID
								LEFT JOIN T_BG_Department as C on A.DepartmentId=C.Id
								LEFT JOIN T_BG_Area AS D ON A.AreaId=D.ID
								LEFT JOIN T_BG_Position AS E ON A.PositionId=E.PositionId
								LEFT JOIN T_Bg_ShortStaticField AS F ON A.DepartmentId=F.ShortId
								LEFT JOIN T_Bg_ShortStaticField AS G ON A.HouseholdId=G.ShortId
								WHERE A.IsDel=0	{0}
								";
				string whereSql = string.Empty;
				DynamicParameters pars = new DynamicParameters();
				if (areaId > 0)
				{
					whereSql += " AND A.AreaId=@AreaId ";
					pars.Add("AreaId", areaId);
				}
				if (shopId > 0)
				{
					whereSql += " AND A.ShopID=@ShopID ";
					pars.Add("ShopID", shopId);
				}
				if (status>0)
				{
					whereSql += " AND A.Status=@Status ";
					pars.Add("Status", status);
				}
				if (!string.IsNullOrEmpty(realName))
				{
					whereSql += " AND A.RealName LIKE @NAME ";
					pars.Add("NAME", string.Format("%{0}%", realName));
				}
				
				sql = string.Format(sql, whereSql);
				var list = _bgUserExtendRepository.QueryList(sql, pars);
				if (list != null)
				{
					return list.ToList();
				}
			}
			catch (Exception ex)
			{
			}
			return null;
		}

		public List<BgUserExtend> GetApproveList(long positionId)
		{
			try
			{
				string sql = @"
								 EXEC GetApproveList @positionId
								";
				
				DynamicParameters pars = new DynamicParameters();
				pars.Add("positionId", positionId);
				var list = _bgUserExtendRepository.QueryList(sql, pars);
				if (list != null)
				{
					return list.ToList();
				}
			}
			catch (Exception ex)
			{
				
			}
			return null;
		}
	}
}

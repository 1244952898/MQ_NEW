using Dapper;
using mq.dataaccess.sql.Interface;
using mq.model.dbentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.application.service
{
	public class BgShortStaticFieldService: IBgShortStaticFieldService
	{
		private string Colnum = @"
									[ShortId]
									,[ShortName]
									,[Type]
									,[Sort]
								";
		private IBgShortStaticFieldRepository _bgShortStaticFieldRepository;
		public BgShortStaticFieldService(IBgShortStaticFieldRepository bgShortStaticFieldRepository)
		{
			_bgShortStaticFieldRepository = bgShortStaticFieldRepository;
		}

		public List<T_Bg_ShortStaticField> GetListByType(int type)
		{
			try
			{
				string sql = @"
								SELECT {0}
								FROM [POS].[dbo].[T_Bg_ShortStaticField]
								WHERE [Type]=@Type
								ORDER BY [Sort] DESC
                                ";
				DynamicParameters dynParams = new DynamicParameters();
				sql = string.Format(sql,Colnum);
				dynParams.Add("@Type", type);
				return _bgShortStaticFieldRepository.QueryList(sql, dynParams).ToList();
			}
			catch (Exception )
			{
			}
			return null;
		}
	}
}

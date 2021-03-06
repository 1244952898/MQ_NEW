﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using mq.application.common;
using mq.dataaccess.sql;
using mq.model.dbentity;

namespace mq.application.service
{
    public class BgUpFilesService : IBgUpFilesService
    {
        IBgUpFilesRepository _bgUpFilesRepository;

        public BgUpFilesService(IBgUpFilesRepository bgUpFilesRepository)
        {
            _bgUpFilesRepository = bgUpFilesRepository;
        }

        public long Add(T_BG_UpFiles bgUpFiles)
        {
            return _bgUpFilesRepository.Add(bgUpFiles);
        }

        public long GetListByUserIdAndFileNameAndExt(string originFileName, long? userid, string ext, int type)
        {
            PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pmain.Predicates.Add(Predicates.Field<T_BG_UpFiles>(f => f.fileoriginname, Operator.Eq, originFileName));
            pmain.Predicates.Add(Predicates.Field<T_BG_UpFiles>(f => f.userid, Operator.Eq, userid));
            pmain.Predicates.Add(Predicates.Field<T_BG_UpFiles>(f => f.ext, Operator.Eq, ext));
            pmain.Predicates.Add(Predicates.Field<T_BG_UpFiles>(f => f.type, Operator.Eq, type));
            return _bgUpFilesRepository.QueryRecordCount(pmain);
        }

        public T_BG_UpFiles GetListByFilename(string filename)
        {
            PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pmain.Predicates.Add(Predicates.Field<T_BG_UpFiles>(f => f.filename, Operator.Eq, filename));
            return _bgUpFilesRepository.GetModel(pmain);
        }

        public T_BG_UpFiles GetListByFilePathForEmail(string filepath,long userid)
        {
            PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pmain.Predicates.Add(Predicates.Field<T_BG_UpFiles>(f => f.filepath, Operator.Eq, filepath));
            pmain.Predicates.Add(Predicates.Field<T_BG_UpFiles>(f => f.userid, Operator.Eq, userid));
            return _bgUpFilesRepository.GetModel(pmain);
        }

        public bool DelFile(T_BG_UpFiles file)
        {
            return _bgUpFilesRepository.Delete(file);
        }

        public bool DelFileByFileNewName(string newName)
        {
            try
            {
                string sql = @"
                            DELETE FROM T_BG_UpFiles 
                            WHERE filename=@filename
                            ";
                SqlParameter par = new SqlParameter("filename", newName);
                return _bgUpFilesRepository.ExecuteSql(sql, par) > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}

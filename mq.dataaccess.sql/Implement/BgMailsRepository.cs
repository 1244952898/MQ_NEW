using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.dataaccess.sql;
using mq.model.dbentity;

namespace mq.dataaccess.sql
{
    public class BgMailsRepository : RepositoryBase<T_BG_Mails>, IBgMailsRepository
    {
    }
}

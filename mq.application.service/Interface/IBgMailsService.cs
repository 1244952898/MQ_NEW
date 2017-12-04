using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service
{
    public interface IBgMailsService
    {
        long Add(T_BG_Mails email);
        List<T_BG_Mails> List(long userId);
        T_BG_Mails Get(long id);
        bool Update(T_BG_Mails email);
    }
}

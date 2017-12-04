using mq.model.dbentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.model.viewentity
{
    public class ShopAddEntity : JsonBaseEntity
    {
       public List<T_BG_Area> areaList { get; set; }
       public List<T_BG_User> userList { get; set; }
    }
}

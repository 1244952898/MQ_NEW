//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace mq.model.dbentity
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_BG_Email
    {
        public long Id { get; set; }
        public long SendUserId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public System.DateTime AddTime { get; set; }
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public int IsDel { get; set; }
        public Nullable<int> Lvl { get; set; }
        public string SendUserName { get; set; }
        public string Recievers { get; set; }
    }
}

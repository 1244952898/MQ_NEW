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
    
    public partial class V_BG_DisplayGuideFile_User
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string FileNewName { get; set; }
        public string FileOriginName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public Nullable<long> UserId { get; set; }
        public Nullable<System.DateTime> PublishTime { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }
        public Nullable<int> IsDel { get; set; }
    }
}

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
    
    public partial class T_BG_User
    {
        public int ID { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string RealName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public Nullable<int> RoleID { get; set; }
        public Nullable<int> ShopID { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> ApproveTime { get; set; }
        public string ApproveName { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }
        public Nullable<int> IsDel { get; set; }
        public Nullable<long> DepartmentId { get; set; }
        public Nullable<long> AreaId { get; set; }
        public int Gender { get; set; }
        public long PositionId { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string IP { get; set; }
        public long EducationId { get; set; }
        public long HouseholdId { get; set; }
        public string School { get; set; }
        public string Emergency { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
        public long Lvl { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KYPCenterManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Payment
    {
        public int Id { get; set; }
        public Nullable<int> TotalStudent { get; set; }
        public Nullable<int> TotalPaidAmount { get; set; }
        public Nullable<int> TotalDueAmount { get; set; }
        public Nullable<int> CenterID { get; set; }
    
        public virtual Center Center { get; set; }
    }
}

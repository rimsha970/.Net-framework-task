//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    
    public partial class ShopKeeper
    {
        public int LoginId { get; set; }
        public int ShopID { get; set; }


        public byte[] picture { get; set; }

        public HttpPostedFileBase ImageFile2 { get; set; }
    
        public virtual Login Login { get; set; }
        public virtual Shop Shop { get; set; }
    }
}

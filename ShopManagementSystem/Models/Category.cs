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
    using System.ComponentModel.DataAnnotations;
    
    public partial class Category
    {
        public Category()
        {
            this.Items = new HashSet<Item>();
        }
    
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        
        public string Name { get; set; }

        [Required]
        
        public string Description { get; set; }
        public int ShopID { get; set; }
    
        public virtual Shop Shop { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
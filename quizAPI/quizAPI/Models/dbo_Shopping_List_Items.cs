//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace quizAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class dbo_Shopping_List_Items
    {
        public int ID { get; set; }
        public Nullable<int> ShoppingListID { get; set; }
        public string itemName { get; set; }
        public string Category { get; set; }
        public Nullable<short> Status { get; set; }
    }
}
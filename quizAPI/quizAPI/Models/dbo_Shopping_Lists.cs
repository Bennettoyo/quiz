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
    
    public partial class dbo_Shopping_Lists
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public string listName { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}

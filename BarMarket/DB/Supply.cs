//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BarMarket.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Supply
    {
        public int ID { get; set; }
        public string Beverage_Name { get; set; }
        public Nullable<int> Beverage_ID { get; set; }
        public string Beverage_Type { get; set; }
        public Nullable<decimal> Beverage_Percent { get; set; }
        public string Beverage_Creator { get; set; }
        public Nullable<int> Supply_ID { get; set; }
        public Nullable<System.DateTime> Supply_Date { get; set; }
        public string Supplier { get; set; }
        public Nullable<long> Quantity { get; set; }
        public Nullable<bool> Activated { get; set; }
        public string Action { get; set; }
        public string Username { get; set; }
        public Nullable<decimal> Beverage_Volume { get; set; }
        public Nullable<decimal> Beverage_Price { get; set; }
    }
}
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
    
    public partial class SellLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public Nullable<long> Quantity { get; set; }
        public string Supplier { get; set; }
        public Nullable<int> SellerId { get; set; }
        public string SellerName { get; set; }
        public Nullable<System.DateTime> Timestamp { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CriticWeb.DataLayerEF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Review
    {
        public System.Guid ReviewId { get; set; }
        public System.Guid UserId { get; set; }
        public System.Guid EntertainmentId { get; set; }
        public byte Point { get; set; }
        public string Opinion { get; set; }
        public System.DateTimeOffset Time { get; set; }
        public string Link { get; set; }
        public string Publication { get; set; }
        public int Helpful { get; set; }
        public int Unhelpful { get; set; }
        public bool CheckedByAdmin { get; set; }
    
        public virtual Entertainment Entertainment { get; set; }
        public virtual UserCritic UserCritic { get; set; }
    }
}

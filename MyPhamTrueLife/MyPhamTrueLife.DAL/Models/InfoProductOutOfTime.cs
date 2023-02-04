﻿using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models
{
    public partial class InfoProductOutOfTime
    {
        public int ProductId { get; set; }
        public int ExpiryProductId { get; set; }
        public int? Amount { get; set; }
        public string Status { get; set; }
        public string BrowsingStatus { get; set; }
        public int? SupplierId { get; set; }
        public int? CapacityId { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoCapacity Capacity { get; set; }
        public virtual InfoExpiryProduct ExpiryProduct { get; set; }
        public virtual InfoProduct Product { get; set; }
        public virtual InfoSupplier Supplier { get; set; }
    }
}

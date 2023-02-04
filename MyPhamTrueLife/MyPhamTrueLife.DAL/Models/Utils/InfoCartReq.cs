using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhamTrueLife.DAL.Models.Utils
{
    public class InfoCartReq
    {

    }
    public class InfoCartRequest
    {
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int? CapacityId { get; set; }
        public int? NatureId { get; set; }
        public int? Quantity { get; set; }
        public int? Total { get; set; }
    }
    public class InfoCartUserShow
    {
        public int CartId { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? CapacityId { get; set; }
        public int? NatureId { get; set; }
        public int? Quantity { get; set; }
        public int? ProductPrice { get; set; }
        public int? Total { get; set; }
        public string Avatar { get; set; }
    }
}

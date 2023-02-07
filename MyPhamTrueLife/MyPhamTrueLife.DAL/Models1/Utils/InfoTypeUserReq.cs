using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhamTrueLife.DAL.Models.Utils
{
    public class InfoTypeUserReq
    {
        public int TypeUserId { get; set; }
        public string TypeUserName { get; set; }
        public int? DiscountPercen { get; set; }

    }
}

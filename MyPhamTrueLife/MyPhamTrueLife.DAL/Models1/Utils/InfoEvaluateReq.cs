using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhamTrueLife.DAL.Models.Utils
{
    public class InfoEvaluateReq
    {

    }
    public class InfoEvaluateRequest
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int? NumberStars { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhamTrueLife.DAL.Models.Utils
{
    public class InfoCommentReq
    {

    }
    public class InfoCommentRequest
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Times { get; set; }
        public string Content { get; set; }
    }
}

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

    public class InfoCommentClient
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Times { get; set; }
        public string Content { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public string FullName { get; set; }
        public string Avatar { get; set; }
    }

    public class InfoEvaluateClient
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int? NumberStars { get; set; }

        public string FullName { get; set; }
        public string Avatar { get; set; }
    }
}

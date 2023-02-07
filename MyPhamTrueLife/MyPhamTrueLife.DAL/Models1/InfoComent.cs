using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoComent
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

        public virtual InfoProduct Product { get; set; }
        public virtual InfoUser User { get; set; }
    }
}

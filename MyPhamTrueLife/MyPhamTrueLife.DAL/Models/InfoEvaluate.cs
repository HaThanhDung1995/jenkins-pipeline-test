using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models
{
    public partial class InfoEvaluate
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int? NumberStars { get; set; }

        public virtual InfoProduct Product { get; set; }
        public virtual InfoUser User { get; set; }
    }
}

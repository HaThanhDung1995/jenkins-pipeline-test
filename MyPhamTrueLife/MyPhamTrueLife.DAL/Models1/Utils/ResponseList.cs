using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhamTrueLife.DAL.Models.Utils
{
    public class ResponseList
    {
        public Paging Paging { get; set; }
        public Object ListData { get; set; }
    }
}

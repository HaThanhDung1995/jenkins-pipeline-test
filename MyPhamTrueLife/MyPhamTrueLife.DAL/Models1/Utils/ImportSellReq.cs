using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhamTrueLife.DAL.Models1.Utils
{
    public class ImportSellReq
    {
    }

    public class InfoDetailImportList
    {
        public int ImportSellId { get; set; }
        public int ProductId { get; set; }
        public InfoProduct infoProduct { get; set; }
        public int? Amount { get; set; }
        public int? Prize { get; set; }
        public int? CapacityId { get; set; }
        public InfoCapacity infoCapacity { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public string Trademark { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhamTrueLife.BLL.Implement
{
    public class InfoProductReq
    {

    }

    public class InfoProductInsertLogin
    {
        public string ProductName { get; set; }
        public string Describe { get; set; }
        public bool? IsExpiry { get; set; }
        public string Trademark { get; set; }
        public int? NatureId { get; set; }
        public bool? IsAroma { get; set; }
        public List<int> ListCapacity { get; set; }
        public List<string> ListImage { get; set; }
        public string Avatar { get; set; }
        public int TypeProductId { get; set; }
    }
}

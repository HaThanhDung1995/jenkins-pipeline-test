﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhamTrueLife.DAL.Models.Utils
{
    public class ResponseList
    {
        public Paging Paging { get; set; }
        public Object ListData { get; set; }
    }

    public class ResponseAffiliateList
    {
        public Paging Paging { get; set; }
        public Object ExtendsData { get; set; }
        public Object ListData { get; set; }
    }
}

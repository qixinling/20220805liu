using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbShopGoods
    {
        public int Id { get; set; }
        public string GoodsNo { get; set; }
        public string Goodsname { get; set; }
        public int Goodstype { get; set; }
        public decimal Goodsprice { get; set; }
        public decimal Goodspv { get; set; }
        public int Stock { get; set; }
        public int Sales { get; set; }
        public string Goodscontent { get; set; }
        public string Goodsimg { get; set; }
        public DateTime Goodsdate { get; set; }
        public int Ispay { get; set; }
        public int Dlid { get; set; }
        public string Dlname { get; set; }
        public int Xlid { get; set; }
        public string Xlname { get; set; }
        public string Goodslabel { get; set; }
        public decimal Cost { get; set; }
        public string Prompt { get; set; }
        public int Sort { get; set; }
        public decimal Tj1 { get; set; }
        public decimal Tj2 { get; set; }
        public decimal Tj3 { get; set; }
        public decimal Tj4 { get; set; }
        public decimal Tj5 { get; set; }
        public int Ishome { get; set; }
        public string Bili { get; set; }
        public decimal Xyzjine { get; set; }
    }
}

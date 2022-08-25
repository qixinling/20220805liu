using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbSystemSetting
    {
        public int Id { get; set; }
        public string Marqueemsg { get; set; }
        public int Switchsystem { get; set; }
        public string Systemclosemsg { get; set; }
        public int? Timestart { get; set; }
        public int? Timeend { get; set; }
        public string Timeclosemsg { get; set; }
        public int Switchtjt { get; set; }
        public int Switchwlt { get; set; }
        public int Switchchongzhi { get; set; }
        public int Switchtixian { get; set; }
        public int Switchzhuanzhang { get; set; }
        public int Switchzhuanhuan { get; set; }
        public string Bank { get; set; }
        public decimal Txmin { get; set; }
        public decimal Txmax { get; set; }
        public int Txbs { get; set; }
        public decimal Txsl { get; set; }
        public decimal Czmin { get; set; }
        public decimal Czmax { get; set; }
        public int Czbs { get; set; }
        public decimal Zzmin { get; set; }
        public decimal Zzmax { get; set; }
        public int Zzbs { get; set; }
        public int Zhbs { get; set; }
        public string Jydays { get; set; }
    }
}

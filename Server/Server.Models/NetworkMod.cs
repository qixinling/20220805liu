using System.Collections.Generic;

namespace Server.Models
{
    public class NetworkMod
    {
        public NetworkMod()
        {
            children = new List<NetworkMod>();
        }
        public int Id { get; set; }
        public string Label { get; set; }
        public int Treeplace { get; set; }
        public string Area { get; set; }
        public int Teamcount { get; set; }
        public decimal Teamyeji { get; set; }
        public decimal Area1 { get; set; }
        public decimal Area2 { get; set; }
        public decimal Area3 { get; set; }
        public decimal Area4 { get; set; }
        public decimal Area5 { get; set; }
        public string Fname { get; set; }
#pragma warning disable IDE1006 // 命名样式
        public List<NetworkMod> children { get; set; } //此处children必须为小写，否则网络图无法显示
#pragma warning restore IDE1006 // 命名样式


    }
}
using System.Collections.Generic;

namespace Server.Models
{
    public class TreeMod
    {
        public TreeMod()
        {
            Children = new List<TreeMod>();
        }
        public int Id { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
        public decimal Teamcount { get; set; }
        public decimal Teamyeji { get; set; }
        public decimal Riteamyeji { get; set; }
        public decimal Lsk { get; set; }
        public List<TreeMod> Children { get; set; }
    }
}
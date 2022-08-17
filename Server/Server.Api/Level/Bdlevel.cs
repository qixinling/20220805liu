using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Level
{
    public class Bdlevel : ILevel
    {
        public int Lx => 2;
        public int Level { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public bool IsOption { get; set; }

        public List<ILevel> GetLevels(DbConnect dbConnect = null)
        {
            return new List<ILevel>
            {
                new Bdlevel{
                    Level = 0,
                    Name = "无",
                    IsOption = true
                },
                new Bdlevel
                {
                    Level = 1,
                    Name = "",
                    IsOption = false
                }
            };
        }

        public void LevelUp(DbConnect dbConnect, string Path = null)
        {
            throw new NotImplementedException();
        }
    }
}

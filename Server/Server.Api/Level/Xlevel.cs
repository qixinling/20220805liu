using Microsoft.EntityFrameworkCore;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Level
{
    public class Xlevel : ILevel
    {
        public int Lx => 1;
        public int Level { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public bool IsOption { get; set; }

        public List<ILevel> GetLevels(DbConnect dbConnect = null)
        {
            return new List<ILevel>
            {
                new Xlevel{
                    Level = 0,
                    Name = "无",
                    IsOption = true
                },
                
            };
        }

        public void LevelUp(DbConnect dbConnect, string Path)
        {
            
        }
    }
}

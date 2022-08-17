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
                new Xlevel
                {
                    Level = 1,
                    Name = "初级",
                    IsOption = true
                },
                new Xlevel
                {
                    Level = 2,
                    Name = "中一级",
                    IsOption = true
                },
                new Xlevel
                {
                    Level = 3,
                    Name = "中二级",
                    IsOption = true
                },
                new Xlevel
                {
                    Level = 4,
                    Name = "中三级",
                    IsOption = true
                },
                new Xlevel
                {
                    Level = 5,
                    Name = "高级",
                    IsOption = false
                }
            };
        }

        public void LevelUp(DbConnect dbConnect, string Path)
        {
            
        }
    }
}
